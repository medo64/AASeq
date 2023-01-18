.PHONY: all clean distclean dist debug release test

all: release

clean:
	@bash ./Make.sh clean

distclean: clean
	@bash ./Make.sh distclean

dist:
	@bash ./Make.sh dist

debug:
	@bash ./Make.sh debug

release:
	@bash ./Make.sh release

test: debug
	@bash ./Make.sh test
