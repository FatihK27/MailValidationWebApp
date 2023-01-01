# Mail Validation WebUI and Worker Service Project.

Hello. 
This project is a project which I made for Huawei Cloud & Patika.dev Practicum. There is a WebUI, and a Worker Service are included in my project which developed with .net core 7 and also you can find basic Identity Server implementation. I used Postgresql as database and developed it as codefirst with Ef core. You can send mail verification requests received via WebUI directly to the RabbitMQ queue. Then you can read the queued messages in the background with the Worker service and send them to the Truemail service. (Thanks Truemail Guys! - https://github.com/truemail-rb/truemail-rack-docker-image). And finally you can post the received mail validation responses to Postgresql. By the way, don't forget to take a look at the services that Huawei Cloud provides us. I used and published my project for test purposes on Huawei Cloud Services. ECS (for WebUI and Worker Service), Cloud Container Engine for Truemail docker images,  DMS for RabbitMQ, RDS for Postgrsql as database. Everything was amazing. You can take a look on http://www.huaweicloud.com

Thanks.
