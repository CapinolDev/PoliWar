module input_handler
    use terminal_io, only: get_key, ESC
    implicit none

    integer, parameter :: K_UP = 1001
    integer, parameter :: K_DOWN = 1002
    integer, parameter :: K_LEFT = 1003
    integer, parameter :: K_RIGHT = 1004
    integer, parameter :: K_QUIT = 1005
    integer, parameter :: K_SHOWFILES = 1006
    integer, parameter :: K_TOGGLETERM = 1007
    integer, parameter :: K_NONE   = 0

contains

    function fetch_action() result(res)
        integer :: res
        character(len=1) :: c1, c2, c3

        c1 = get_key()

        if (c1 == ESC) then
            c2 = get_key()
            c3 = get_key()

            if (c2 == '[') then
                select case (c3)
                    case ('A'); res = K_UP
                    case ('B'); res = K_DOWN
                    case ('C'); res = K_RIGHT
                    case ('D'); res = K_LEFT
                    case default; res = K_NONE
                end select
            else
                res = K_NONE
            end if
        else if (c1 == 'q' .or. c1 == 'Q') then
            res = K_QUIT
        else if (c1 == 'f' .or. c1 == 'F') then
            res = K_SHOWFILES
        else if (c1 == 't' .or. c1 == 'T') then
            res = K_TOGGLETERM
        else
            res = ichar(c1)
        end if
    end function fetch_action
end module input_handler
