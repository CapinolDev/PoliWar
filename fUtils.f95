module utils
	implicit none
	contains
	subroutine testAskF() bind(c, name='Fask')
		write(*,'(A)') "testty"
	end subroutine testAskF
	
end module utils
