module utils
	use iso_c_binding
	implicit none
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
end module utils
