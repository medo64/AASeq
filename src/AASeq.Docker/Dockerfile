FROM mcr.microsoft.com/dotnet/runtime:9.0-alpine

USER root

# Environment
ENV ENV="/etc/profile"
RUN echo "alias ll='ls -la'" > /etc/profile.d/00-aliases.sh

# Bash
COPY src/AASeq.Docker/bash-default.sh /etc/bash/default.sh

# Helper packages
RUN apk add bash bind-tools tcpdump

# Executables
WORKDIR /app
COPY src/AASeq.Docker/aaseq.sh aaseq
RUN chmod +x aaseq
COPY bin/linux-musl-x64/* bin/
RUN chmod +x bin/AASeq

# Done
ENTRYPOINT [ "bash" ]
