# SendEmailWebApi
Projet with a simple implementation using [FluentEmail](https://github.com/lukencode/FluentEmail), showing your facilities to custom and send email with SMTP configuration.

In this sample, the [FluentEmail.Smtp](https://github.com/lukencode/FluentEmail/tree/master/src/Senders/FluentEmail.Smtp) was custom to attached imagens inline as part of Html body.


## Conteiners
### SendEmail.Domain

* Entities and interfaces.

### SendEmail.WebApi

* Presenter, configurations and controllers.

### SendEmail.FluentEmailAdapter

* Dependencies
  * FluentEmail.Core 3.0.2
  * FluentEmail.Razor 3.0.2
  * FluentEmail.Smtp 3.0.2
  * Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation 5.0.17