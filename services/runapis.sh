trap "exit" INT TERM ERR
trap "kill 0" EXIT


# This are for running docker RabbitMQ image ISN'T IMPLEMENTED
# p - local port for docker RabbitMQ container
# h - hostname for docker
# v - version of RabbitMQ container
# d - detatch mode for docker
# n - name for docker container

detachmode=false

while getopts p:h:v:n:d flag; 
do
        case "${flag}" in
                p) port=${OPTARG};;
                h) hostname=${OPTARG};;
                v) containerversion=${OPTARG};;
                d) detachmode=true;;
                n) nameofcontainer=${OPTARG};;
        esac
done


paths=$(find . -iname "*Api.csproj")

echo "Projects are starting...."

for csprojpath in $paths
do
        fullpath=$(readlink -f $csprojpath)
        dotnet run --project $fullpath &
        pid=$!
                count=$(ps -A| grep $pid |wc -l)
                if [ $count -eq 0 ] 
                        then
                                if wait $pid; 
                                then
                                        echo "success"
                                else                   
                                        echo "failed (returned $?)"
                                fi
                        else
                        echo "success"
                fi
done

echo "wait"

# sudo docker run 

wait

