FC = gfortran
FFLAGS = -shared -fPIC
TARGET_DIR = PoliWar/bin/Debug/net10.0/
TARGET_LIB = $(TARGET_DIR)libfUtils.so

FORTRAN_SRC = terminalio.f95 ui.f95 input.f95 fUtils.f95 

all: $(TARGET_LIB) build_dotnet

$(TARGET_LIB): $(FORTRAN_SRC)
	mkdir -p $(TARGET_DIR)
	$(FC) $(FFLAGS) -o $(TARGET_LIB) $(FORTRAN_SRC)

build_dotnet:
	dotnet build PoliWar

clean:
	rm -f $(TARGET_LIB)
	rm -f *.mod
	rm -rf PoliWar/bin PoliWar/obj
