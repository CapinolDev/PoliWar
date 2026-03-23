module terminal_io
    use iso_c_binding
    implicit none

    character(len=1), parameter :: ESC = char(27)

    integer(c_int), parameter :: STDIN_FILENO = 0
    integer(c_int), parameter :: TCGETS = int(Z'5401', c_int)
    integer(c_int), parameter :: TCSETS = int(Z'5402', c_int)
    integer(c_int), parameter :: ICANON_FLAG = 2  
    integer(c_int), parameter :: ECHO_FLAG   = 8  

    type, bind(c) :: termios
        integer(c_int) :: c_iflag, c_oflag, c_cflag, c_lflag
        character(c_char) :: c_line
        character(c_char) :: c_cc(32)
        integer(c_int) :: c_ispeed, c_ospeed
    end type termios

    type(termios) :: original_term

    interface
        integer(c_int) function ioctl(fd, request, arg) bind(c, name="ioctl")
            import :: c_int
            integer(c_int), value :: fd
            integer(c_int), value :: request
            type(*) :: arg
        end function ioctl

        integer(c_int) function system_read(fd, buf, count) bind(c, name="read")
            import :: c_int, c_char
            integer(c_int), value :: fd
            character(c_char) :: buf
            integer(c_int), value :: count
        end function system_read
    end interface

contains

    subroutine enter_raw_mode() bind(c, name="enter_raw_mode")
        type(termios) :: raw
        integer(c_int) :: res

        res = ioctl(STDIN_FILENO, TCGETS, original_term)
        raw = original_term
        
        raw%c_lflag = iand(raw%c_lflag, not(ICANON_FLAG)) 
        raw%c_lflag = iand(raw%c_lflag, not(ECHO_FLAG)) 

        res = ioctl(STDIN_FILENO, TCSETS, raw)
    end subroutine enter_raw_mode

    subroutine exit_raw_mode()
        integer(c_int) :: res
        res = ioctl(STDIN_FILENO, TCSETS, original_term)
    end subroutine exit_raw_mode

    function get_key() result(char_out)
        character(len=1) :: char_out
        character(c_char) :: c_buf
        integer(c_int) :: res
        
        res = system_read(STDIN_FILENO, c_buf, 1_c_int)
        char_out = c_buf
    end function get_key

end module terminal_io

