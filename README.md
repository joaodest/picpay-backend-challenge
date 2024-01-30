# Backend Challenge - Picpay

## Introdução
O sistema consiste em tratar da infraestrutura de transações bancárias entre um usuário e outro, passar por validações externas e tornar possível a reversão de transações em qualquer problema. O padrão de desenvolvimento adotado foi DDD (Domain-Driven Development), permitindo segregar responsabilidades de usuário e operações bancárias.

## Tecnologias
Optei por utilizar o .NET 8 como principal tecnologia, a facilidade oferecida pelo Entity Framework foi pensada para ter um mapeamento consistente de dados, principalmente por se tratar de dados potencialmente sensiveis. O banco de dados utilizado foi o MySQL por proporcionar uma estruturação mais robusta.

## Configuração
O sistema tem algumas dependências como o AutoMapper, Entity Framework e o próprio MySQL, caso opte por utilizar o docker-compose, não devem ser problema.

## Uso
Por utilizar o MySQL, quando usar o comando `docker-compose up`, o script está configurado para aguardar 15 segundos até que o database se inicialize. Portanto, caso essa configuração não funcione corretamente ou você opte por remover o "wait-for-it", o EF pode tentar se conectar ao MySQL antes que ele esteja apto a permitir essa conexão.

Ao criar os usuários, algumas informações sensíveis não estarão visíveis, e todo usuário terá um histórico de transações enviadas e recebidas. É possível rastrear usuários pelo id ou pelo documento (cpf ou cnpj), uma vez que, é apenas possivel registrar um documento no banco de dados.

As transações são rastreáveis a partir do seu ID único, além de serem registradas no histórico dos usuários, uma tabela contendo todas as transações também está disponível.

## Funcionalidades
A proposta do desafio era ser uma API RESTful, portanto, é possível encontrar essa implementação no conjunto de endpoints do usuário. Os endpoints de transações devem ser consideradas rotinas internas, que não estariam disponiveis para os usuários, e o endpoint de exclusão de uma transação especialmente deve ser utilizada apenas como teste, já que num cenário real, transações devem se manter persistentes. Além disso, a reversão de cada transação é uma rotina interna, então não é diretamente acessível.

## Contribuição
Fique à vontade para abrir issues em lógica de negócio ou fluxo de eventos. Como dito, optei por utilizar DDD para manter as responsabilidades o mais separadas quanto possível.
