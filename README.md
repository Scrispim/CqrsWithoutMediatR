Usando CQRS sem MediatR no ASP.NET Core

Como isso funciona "por baixo dos panos"?

Não existe nenhum "middleware de roteamento" como no MediatR com .Send(). Em vez disso:
* Você registra seus handlers explicitamente no DI.
* O ASP.NET Core faz a injeção direta no controller.
* Não há reflection ou busca dinâmica de handlers. Tudo é fortemente tipado e resolvido estaticamente.

Não há magia aqui. Você está só usando boas práticas de separação de responsabilidades e interfaces, mas sem nenhum middleware interceptando ou redirecionando a chamada como o MediatR faz.


Vantagens dessa abordagem:
* Código mais explícito, mais fácil de depurar.
* Sem dependência do MediatR (ou se usar, usa apenas como utilitário).
* Cada handler é injeção direta, sem roteamento interno.
* Mais testável, pois a dependência é clara.
* Você pode aplicar políticas como retry, logging, etc. diretamente com decoradores no DI se quiser.
