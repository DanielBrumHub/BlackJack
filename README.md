
# BlackJack

Conjunto de duas API's para jogar BlackJack, o famoso 21 nos cassinos.


## API'S

As API's estão configuradas no swagger e podem ser usadas nas seguintes rotas: 

#### Iniciar partida

```http
  POST/jogo
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `nomeJogador` | `string` | **Obrigatório**. Nome do jogador que vai começar a partida. |

#### Continuar jogo

```http
  PUT/jogo
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `idJogo`      | `int` | **Obrigatório**. Identificação do jogo obtida no retorno da api de início. |
| `continua`      | `bool` | **Obrigatório**. Informa se o jogador deseja mais uma carta, ou se deseja parar e obter o resultado. |




## Rodar Localmente

Para rodar localmente é necessário ter instalado:
- SQLServer Express
- ASP.Net Core 6

Os scripts de criação do ambiente estão na pasta `blackjack\Scripts`
## Testes unitários

Os testes unitários se encontram incompletos nessa branch e serão adicionados posteriormente.

Realizados com o proprio **XUnit**, em um projeto a parte na solution.



## Otimizações 

Otimizações já identificadas a serem feitas:

- Alterar Dapper para EntityFramework.
- Refatorar tratamento de exceções.

