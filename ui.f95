module ui_lib
    use terminal_io, only: ESC  
    implicit none


    character(len=*), parameter :: TOP_L = char(226)//char(150)//char(136)
    character(len=*), parameter :: TOP_R = char(226)//char(149)//char(151)  
    character(len=*), parameter :: BOT_L = char(226)//char(149)//char(154)  
    character(len=*), parameter :: BOT_R = char(226)//char(149)//char(157)  
    character(len=*), parameter :: HORIZ = char(226)//char(149)//char(144)  
    character(len=*), parameter :: VERTI = char(226)//char(149)//char(145)  
    character(len=*), parameter :: RESET = char(27)//"[0m"

contains
	subroutine move_cursor(r, c)
		integer, intent(in) :: r, c
		write(*, '(*(g0))', advance='no') ESC, '[', r, ';', c, 'H'
	end subroutine move_cursor

	subroutine draw_box(row, col, width, height, title, color_code)
		integer, intent(in) :: row, col, width, height
		character(len=*), intent(in) :: title, color_code
		integer :: i

		call move_cursor(row, col)
		write(*, '(*(g0))', advance='no') color_code, TOP_L
		do i = 1, width - 2
			write(*, '(*(g0))', advance='no') HORIZ
		end do
		write(*, '(*(g0))', advance='no') TOP_R
		do i = 1, height - 2
			call move_cursor(row + i, col)
			write(*, '(*(g0))', advance='no') VERTI
			call move_cursor(row + i, col + width - 1)
			write(*, '(*(g0))', advance='no') VERTI
		end do
		call move_cursor(row + height - 1, col)
		write(*, '(*(g0))', advance='no') BOT_L
		do i = 1, width - 2
			write(*, '(*(g0))', advance='no') HORIZ
		end do
		write(*, '(*(g0))', advance='no') BOT_R
		call move_cursor(row, col + 2)
		write(*, '(*(g0))', advance='no') ' ', title, ' ', RESET
	end subroutine draw_box
end module ui_lib
