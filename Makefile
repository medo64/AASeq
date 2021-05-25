.PHONY: all clean distclean dist debug release test

all:
	@./Make.sh all

clean:
	@./Make.sh clean

distclean:
	@./Make.sh distclean

dist:
	@./Make.sh dist

debug:
	@./Make.sh debug

release:
	@./Make.sh release

test:
	@./Make.sh test

web:
	@./Make.sh web
