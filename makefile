FC = gfortran
FFLAGS = -shared -fPIC
TARGET_LIB = PoliWar/bin/Debug/net10.0/libfUtils.so
FORTRAN_SRC = fUtils.f95

all: $(TARGET_LIB) build_dotnet


$(TARGET_LIB): $(FORTRAN_SRC)
	mkdir -p PoliWar/bin/Debug/net10.0/
	$(FC) $(FFLAGS) -o $(TARGET_LIB) $(FORTRAN_SRC)

build_dotnet:
	dotnet build PoliWar

clean:
	rm -f $(TARGET_LIB)
	rm -rf PoliWar/bin PoliWar/obj
