run:
	docker-compose up -d

state:
	docker-compose ps

stop:
	docker-compose stop

remove:
	docker-compose down --volumes

remove-img:
	docker rmi -f $(sudo docker images -q)

