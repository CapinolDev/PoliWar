module utils
	use iso_c_binding
	use ui_lib
	use terminal_io
	use input_handler
	implicit none
	
	character(len=*), parameter :: HIDE_CURSOR = char(27)//"[?25l"
	character(len=*), parameter :: SHOW_CURSOR = char(27)//"[?25h"
	contains
	subroutine errExitF(exitCode) bind(c,name='FerrExit')
		integer(c_int), value :: exitCode
		error stop exitcode
	end subroutine
	function fileUtilF(opcode) bind(c,name='FfileUtil')
		integer(c_int), value :: opCode
		integer, parameter :: FILE_SAVE = 1
		integer, parameter :: FILE_LOAD = 2
		integer :: fileUtilF
		fileUtilF = 2
		if (opCode == FILE_SAVE) then
			write(*,'(A)') "SAVING"
			fileUtilF=0
		else if(opCode == FILE_LOAD) then
			write(*,'(A)') "LOADING"
			fileUtilF=0
		end if
	end function fileUtilF
	subroutine enterRawModeF() bind(c,name='FenterRawMode')
		call enter_raw_mode()
	end subroutine enterRawModeF
	subroutine exitRawModeF() bind(c,name='FexitRawMode')
		call exit_raw_mode()
	end subroutine exitRawModeF
	subroutine hideCursorF() bind(c,name='FhideCursor')
		write(*, '(A)', advance='no') HIDE_CURSOR
	end subroutine hideCursorF
	subroutine showCursorF() bind(c,name='FshowCursor')
		write(*, '(A)', advance='no') SHOW_CURSOR
	end subroutine showCursorF
	subroutine drawBoxF(x,y,width,height,title) bind(c,name='FdrawBox')
		integer(c_int), value :: x,y,width, height
		character(kind=c_char), intent(in) :: title(*)
		character(len=:), allocatable      :: fortran_title
		integer :: i, n
		n = 0
		do while (title(n+1) /= c_null_char)
			n = n + 1
		end do
		allocate(character(len=n) :: fortran_title)
		do i = 1, n
			fortran_title(i:i) = title(i)
		end do
		call draw_box(int(x), int(y), int(width), int(height),fortran_title, char(27)//"[32m")
	end subroutine drawBoxF
	function fetch_actionF() bind(c,name='FfetchAction')
		character(len=1) :: fetch_actionF
		fetch_actionF = get_key() // c_null_char
	end function fetch_actionF
	subroutine move_cursorF(x,y) bind(c,name='FmoveCursor')
		integer(c_int), value :: x,y
		call move_cursor(int(x),int(y))
	end subroutine move_cursorF
	
end module utils
