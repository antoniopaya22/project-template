# Microservices Project Template

![Docker Compose Actions Workflow](https://github.com/antonioalfa22/project-template/workflows/Docker%20Compose%20Actions%20Workflow/badge.svg)
![GitHub contributors](https://img.shields.io/github/contributors/antonioalfa22/project-template?color=green)
![GitHub forks](https://img.shields.io/github/forks/antonioalfa22/project-template)
![GitHub Repo stars](https://img.shields.io/github/stars/antonioalfa22/project-template)
![GitHub issues](https://img.shields.io/github/issues-raw/antonioalfa22/project-template)
![GitHub All Releases](https://img.shields.io/github/downloads/antonioalfa22/project-template/total)

Microservices Project template with Prometheus, Grafana, Node-Exporter, .NET Core Backend and VueJS Frontend.

![Grafana Dashboard](img/grafana.png "Grafana Dashboard")
![Frontend1](img/frontend1.png "Frontend 1")
![Frontend2](img/frontend2.png "Frontend 2")
![Frontend3](img/frontend3.png "Frontend 3")
---

## Building and running

```
make run
```

Once the script is executed, the following urls will be accessible:

- **backend:** http://backend:5000 / http://127.0.0.1:5000
- **frontend:** http://frontend / http://127.0.0.1
- **grafana:** http://grafana:3000 / http://127.0.0.1:3000

## Check containers

```
make status
```

## Clean docker images and containers

Clean containers:
```
make remove
```

Clean images:
```
make remove-img
```

### Setting up Grafana

- **User**: admin
- **Password**: antonio (It can be modified in the docker-compose.ym file)

> It is recommended to create a new Dashboard specifically for node-export methods. To do this you must go to "Import" and enter the Dashboard code 1860