## Courier-hub application - Swift Parcel

## General description
Swift Parcel is a multitier application built with .NET and React. Its target is to create web interface for courier company and its users. User can create an inquiry which contains information about parcel which has to be transported from source to destination. Then, we send it to other companies to get offers from them, also prepare our own one to give an opportunity for user to choose the best one. If user chooses us, our office worker approves or rejects that offer and in case of approval user can track the order. We have also side for couriers where they can assign deliveries for them and change its status during their work.

## Frontend
- [ ] Written in TypeScript using [React](https://react.dev/) from [Node.js](https://nodejs.org/en) environment
- [ ] Components responsible for aesthetics imported from [Flowbite React](https://www.flowbite-react.com) library

## Backend
- [ ] Written in C# using [.NET 6](https://dotnet.microsoft.com/en-us/)
- [ ] Created with concept of microservice architecture
- [ ] Based on [Convey](https://github.com/snatch-dev/Convey) - lightweight set of libraries for building .NET microservices
- [ ] CQRS pattern - using a different model to update data than the model to read data
- [ ] Composed of the following services:
    - [ ] Identity Service - authentication and authorization of all users
    - [ ] Customers Service - storing information about users
    - [ ] Parcels Service - processing inquiries (which allow user to define information about parcel to post)
    - [ ] Orders Service - processing orders (which allow user to choose an offer and track its status after being approved by office worker)
    - [ ] Pricing Service - preparing an offer from the side of Swift Parcel courier company
    - [ ] Couriers Service - storing information about couriers
    - [ ] Deliveries Service - processing deliveries (which allow couriers to change status of order collected by user)
- [ ] There are also services for intergration with APIs of external courier companies:
    - [ ] ExternalAPI.Lecturer - getting an offer from the side of Mini Currier courier company
    - [ ] ExternalAPI.Baronomat - getting an offer from the side of Baronomat courier company
- [ ] API.Gateway created with [Ntrada](https://github.com/snatch-dev/Ntrada) to clip endpoints from microservices to be accessible by one port

## Infrastructure
- [ ] [MongoDB](https://www.mongodb.com/products/platform/cloud) - document-oriented database
- [ ] [Consul](https://www.consul.io) - microservices discovery
- [ ] [RabbitMQ](https://www.rabbitmq.com) - message broker
- [ ] [Fabio](https://github.com/fabiolb/fabio) - load balancing
- [ ] [Jaeger](https://www.jaegertracing.io) - distributed tracing
- [ ] [Grafana](https://grafana.com) - metrics extension
- [ ] [Prometheus](https://prometheus.io) - metrics extension
- [ ] [Seq](https://datalust.co/seq) - logging extension
- [ ] [Vault](https://www.vaultproject.io) - secrets extension