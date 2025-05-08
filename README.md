AASeq (Automated Action Sequence)
=================================

[AASeq](https://aaseq.com) is a protocol simulator intended mostly for functional testing.

<br>

The easiest way to play with it is to use [docker image](https://hub.docker.com/r/aaseq/aaseq).

You can run it locally using something like this:
~~~sh
docker run --rm -it aaseq/aaseq:unstable
~~~

If you want to use tcpdump from within the container, you need to give it a few permissions:
~~~sh
docker run --rm -it --cap-add=NET_ADMIN --cap-add=NET_RAW aaseq/aaseq:unstable
~~~

Once in image you can run tests using one of examples or your own test scenario:

~~~sh
aaseq examples/Ping-DNSv4.aaseq
~~~
