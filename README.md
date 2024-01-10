# Desafio Técnico - .Net/C#

A prova técnica de desenvolvimento back-end é focada na construção de uma API REST voltada ao
contexto de vídeo-monitoramento que atenda aos requisitos descritos abaixo:

## Requisitos funcionais

- Criar um novo servidor
-  Um servidor é composto por ID (guid), nome (string), endereço IP (string), porta IP (integer)
- Atualizar um servidor
- Remover um servidor
- Recuperar um servidor
- Checar disponibilidade de um servidor
-- Verificar se o servidor responde no endereço e porta IP configurados previamente
- Listar todos os servidores
- Adicionar vídeo à um servidor
-- Um arquivo de vídeo é composto por ID (guid), descrição (string) e conteúdo binário do vídeo
- Remover um vídeo
- Recuperar dados cadastrais de um vídeo
- Recuperar conteúdo binário de um vídeo
- Listar dados cadastrais de todos os vídeo de um servidor
- Reciclar vídeo antigos
-- Remover os vídeo adicionados a mais de X dias, incluindo o conteúdo binário do vídeo

## Requisitos não-funcionais

- Todas as operações devem utilizar formato Json (application/json)
-- Conteúdo binário dos vídeos deve ser enviado/recuperado em formato base64
- Todas as operações devem utilizar verbos HTTP adequados à operação
- Todas as operações devem utilizar status HTTP de retorno adequados à operação
- Persistência das informações pode ser realizada em banco de dados ou sistema de arquivos
-- Conteúdo binário dos vídeos deve ser mantido no sistema de arquivos
- Reciclagem de vídeos antigos deve ser realizada em segundo plano (não-bloqueante)
-- Executar a reciclagem em segundo plano e retornar imediatamente o status HTTP 202

### URIs das operações devem ser as seguintes
 - Criar um novo servidor
--  /api/server
- Remover um servidor existente
-- /api/servers/{serverId}
- Recuperar um servidor existente
-- /api/servers/{serverId}
- Checar disponibilidade de um servidor
-- /api/servers/available/{serverId}
- Listar todos os servidores
-- /api/servers
- Adicionar um novo vídeo à um servidor
-- /api/servers/{serverId}/videos
- Remover um vídeo existente
-- /api/servers/{serverId}/videos/{videoId}
- Recuperar dados cadastrais de um vídeo
-- /api/servers/{serverId}/videos/{videoId}
- Download do conteúdo binário de um vídeo
-- /api/servers/{serverId}/videos/{videoId}/binary
- Listar todos os vídeos de um servidor
-- /api/servers/{serverId}/videos
- Reciclar vídeos antigos
-- /api/recycler/process/{days}
-- /api/recycler/status
 ### Exemplos de conteúdo Json (application/json)
- Servidor
```json
{ 
	id:"00A077F6-6ACF-41AF-942A-501BE8FB80F8", 
	name:"Servidor 1", 
	ip:"127.0.0.1", 
	port:8080 
}
```

- Vídeo (informações básicas)
```json
{ 
	id:"11A077F6-6ACF-41AF-942A-501BE8FB80B8", 
	description:"Video 1", 
	sizeInBytes:"291923", 	
}
```

- Reciclagem (status de processamento)
```json
{ 
	status:"running", 	
}
```


```json
{ 
	status:"not running", 	
}
```

# Comentários

- Foi implementado Swagger para documentar os endpoints requeridos e fazer uma breve descrição do mesmo;
- Projeto desenvolvido usando Entity Framework com o Banco SQLlite;
- Binários em Base64 salvos na pasta ./Video do projeto;
