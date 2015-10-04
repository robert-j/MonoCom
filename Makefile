all:	libtest.so managed.exe

libtest.so: native.cpp
	g++ -g -Wall -fPIC -shared -o $@ $<

managed.exe: managed.cs
	mcs /out:$@ $<

run-tests: all
	mono managed.exe

clean:
	rm -f *.exe *.dll  *.o *.so *.a *~
