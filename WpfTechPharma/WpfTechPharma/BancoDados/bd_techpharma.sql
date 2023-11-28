#PROJETO TECHPHARMA

#3°B Informática

#Integrantes:

-- Andrey Medeiros Gois
-- Breno Alcides Paio de Medeiros
-- Giuila Novais Vieira
-- Mailon Camargo Gonçalves
-- Pedro Henrique dos Santos Xavier Araujo
-- Thayna Albuquerque Silva
-- Vinicius de Nano Holanda

create database bd_techpharma;
use bd_techpharma;

create table Endereco(
ende_id int primary key not null auto_increment,
ende_estado varchar (200),
ende_cidade varchar (50),
ende_bairro varchar (50),
ende_rua varchar (50),
ende_numero int,
ende_complemento varchar (200),
ende_cep varchar (20)
);

delimiter $$
create procedure cadastrar_endereco (estado varchar (200), cidade varchar (50), bairro varchar (50), rua varchar(50), numero int, complemento varchar (200), cep varchar(20)) 
begin
	if (estado <> '') and (estado is not null) then
		insert into Endereco values (null, estado, cidade, bairro, rua, numero, complemento, cep);
        select 'Cadastro realizado com sucesso!' as 'Sucesso!';
	else 
		select 'Não foi possível cadastrar os dados! Verifique as informações dentro do campo.' as Erro;
	end if;
end;
$$ delimiter ;

call cadastrar_endereco('RO', 'Ji-Paraná', 'Jardim Presidencial', 'Rua Rondônia', 101, 'Residência', '76901-082');
call cadastrar_endereco('RO', 'Ji-Paraná', 'Jorge Teixeira', 'Rua Nova Iguaçú', 202, 'Residência', '76912-647');
call cadastrar_endereco('RO', 'Ji-Paraná', 'Milão', 'Rua Trinta de Abril', 303, 'Residência', '76901-658');

create table Fornecedor (
forn_id int primary key not null auto_increment,
forn_razao_social varchar (100),
forn_nome_fantasia varchar (100),
forn_cnpj varchar(25),
forn_email varchar (50),
forn_contato varchar (20),
fk_ende_id int,
foreign key (fk_ende_id) references Endereco (ende_id)
);

delimiter $$
create procedure cadastrar_fornecedor (razao_social varchar (50), nome_fantasia varchar (100), cnpj varchar(25), email varchar (50), contato varchar (20), endereco int) 
begin
	if (razao_social <> '') and (razao_social is not null) then
		insert into Fornecedor values (null, razao_social, nome_fantasia, cnpj, email, contato, endereco);
        select 'Cadastro realizado com sucesso!' as 'Sucesso!';
	else
		select 'Não foi possível cadastrar os dados! Verifique as informaçõs dentro do campo.' as Erro;
	end if;
end;
$$ delimiter ;

call cadastrar_fornecedor('Farmácia Preço Baixo', 'Farma Preço Baixo', '81.966.053/0001-63', 'precobaixo@gmail.com', '(69) 2163-0833', 1);
call cadastrar_fornecedor('Drogaria Fabulosa', 'Droga Fabulosa', '75.949.526/0001-11', 'drogafabulosa@gmail.com', '(69) 2222-3556', 2);
call cadastrar_fornecedor('Equipamentos Farmáceuticos', 'Equi Farm', '43.387.110/0001-60', 'equifarm@hotmail.com', '(69) 3926-8996', 3);

create table Cliente (
clie_id int primary key not null auto_increment,
clie_nome varchar (50),
clie_sexo varchar (20),
clie_nascimento date,
clie_rg varchar (25),
clie_cpf varchar (25),
clie_email varchar (50),
clie_contato varchar (20),
fk_ende_id int,
foreign key (fk_ende_id) references Endereco (ende_id)
);

delimiter $$
create procedure cadastrar_cliente (nome varchar (50), sexo varchar (20), nascimento varchar(25), rg varchar (25), cpf varchar (25), email varchar (50), contato varchar (20), endereco int) 
begin
	if (nome <> '') and (nome is not null) then
		insert into Cliente values (null, nome, sexo, nascimento, rg, cpf, email, contato, endereco);
        select 'Cadastro realizado com sucesso!' as 'Sucesso!';
	else
		select 'Não foi possível cadastrar os dados! Verifique as informaçõs dentro do campo.' as Erro;
	end if;
end;
$$ delimiter ;

call cadastrar_cliente('Thayna Albuquerque', 'Não-Binário', '1987-01-01', '23.041.701-2', '904.573.800-71', 'patata@gmail.com', '(69) 93559-0250', 1);
call cadastrar_cliente('Henrique Xavier', 'Masculino', '1996-05-12', '47.563.811-6', '373.472.700-65', 'henryck@gmail.com', '(69) 92233-0163', 2);
call cadastrar_cliente('Breno Paio', 'Masculino', '2001-10-24', '42.200.858-8', '140.293.830-68', 'breninin@gmail.com', '(69) 93447-4291', 3);

create table Funcionario (
func_id int primary key not null auto_increment,
func_nome varchar (50),
func_sexo varchar (20),
func_nascimento date,
func_rg varchar (20),
func_cpf varchar (20),
func_email varchar (50),
func_contato varchar (20),
func_funcao varchar(50),
func_salario float,
fk_ende_id int,
foreign key (fk_ende_id) references Endereco (ende_id)
);

delimiter $$
create procedure cadastrar_funcionario (nome varchar (50), sexo varchar (20), nascimento date, rg varchar (20), cpf varchar (20), email varchar (50), contato varchar (20), funcao varchar (50), salario float, endereco int) 
begin
	if (nome <> '') and (nome is not null) then
		insert into Funcionario values (null, nome, sexo, nascimento, rg, cpf, email, contato, funcao, salario, endereco);
        select 'Cadastro realizado com sucesso!' as 'Sucesso!';
	else
		select 'Não foi possível cadastrar os dados! Verifique as informaçõs dentro do campo.' as Erro;
	end if;
end;
$$ delimiter ;

call cadastrar_funcionario('Mailon Camargo', 'Masculino', '1999-04-01', '20.646.131-8', '155.276.090-18', 'mailon@outlook.com', '(69) 92265-9256', 'Farmaceutico', 2400.0, 1);
call cadastrar_funcionario('Vinicius Nano', 'Masculino', '2001-11-09', '49.945.684-1', '784.954.220-77', 'nanoprime@hotmail.com', '(69) 92982-7586', 'Caixa', 1600.0, 1);
call cadastrar_funcionario('Andrey Medeiros', 'Masculino', '1985-03-06', '31.127.232-0', '447.697.870-30', 'haadty@gmail.com', '(69) 93227-0858', 'Gerente', 3500.0, 1);

create table Usuario (
usua_id int primary key not null auto_increment,
usua_login varchar (20),
usua_senha varchar (20),
fk_func_id int,
foreign key (fk_func_id) references Funcionario (func_id)
);

delimiter $$
create procedure cadastrar_usuario (login varchar (20), senha varchar (20), funcionario int)
begin
	if (login <> '') and (login is not null) then
		insert into Usuario values (null, login, senha, funcionario);
        select 'Cadastro realizado com sucesso!' as 'Sucesso!';
	else
		select 'Não foi possível cadastrar os dados! Verifique as informaçõs dentro do campo.' as Erro;
	end if;
end;
$$ delimiter ;

call cadastrar_usuario('Mailon', 'Abcde123', 1);
call cadastrar_usuario('ViniNano731', 'Prime', 2);
call cadastrar_usuario('Haadty', 'Divinur', 3);

create table Produto (
prod_id int primary key not null auto_increment,
prod_nome varchar (50),
prod_marca varchar (50),
prod_valor_compra float,
prod_valor_venda float,
prod_quantidade int,
prod_tipo varchar (50),
prod_codigo_barra varchar (50),
fk_forn_id int,
foreign key (fk_forn_id) references Fornecedor (forn_id)
);

delimiter $$
create procedure cadastrar_produto (nome varchar(50), marca varchar(50), valor_compra float, valor_venda float, quantidade int, tipo varchar(50), codigo_barra varchar(50), fornecedor int)
begin
	if (nome <> '') and (nome is not null) then
		insert into produto values (null, nome, marca, valor_compra, valor_venda, quantidade, tipo, codigo_barra, fornecedor);
		select 'Cadastro realizado com sucesso!' as 'Sucesso!';
    else
		select 'Não foi possível cadastrar os dados! Verifique as informaçõs dentro do campo.' as Erro;
    end if;
end;
$$ delimiter ;

call cadastrar_produto('Fralda', 'Papmers', 10, 12, 10, 'Tipo A', '1239687150', 1);
call cadastrar_produto('Chocolate', 'Hershey', 6, 8, 5, 'Tipo B', '9871567890', 2);
call cadastrar_produto('Shampoo', 'Seda', 5, 7.5, 3, 'Tipo C', '9751863098', 3);

create table Medicamento (
medi_id int primary key not null auto_increment,
medi_nome varchar (50),
medi_marca varchar (20),
medi_valor_compra float,
medi_valor_venda float,
medi_quantidade int,
medi_tarja varchar (20),
medi_codigo_barra varchar (50),
fk_forn_id int,
foreign key (fk_forn_id) references Fornecedor (forn_id)
);

delimiter $$
create procedure cadastrar_medicamento (nome varchar(50), marca varchar(50), valor_compra float, valor_venda float, quantidade int, tarja varchar(50), codigo_barra varchar(50), fornecedor int)
begin
	if (nome <> '') and (nome is not null) then
		insert into medicamento values (null, nome, marca, valor_compra, valor_venda, quantidade, tarja, codigo_barra, fornecedor);
		select 'Cadastro realizado com sucesso!' as 'Sucesso!';
    else
		select 'Não foi possível cadastrar os dados! Verifique as informaçõs dentro do campo.' as Erro;
    end if;
end;
$$ delimiter ;

call cadastrar_medicamento('Tylenol', 'Tylenol', 30.0, 40.0, 6, 'Sem tarja', '1235398890', 1);
call cadastrar_medicamento('Rivotril', 'Roche', 55.0, 75.0, 4, 'Tarja Preta', '9874517890', 1);
call cadastrar_medicamento('Ivermectina', 'Vitamedi', 40.0, 60.0, 8, 'Tarja Vermelha', '1234569347', 1);

create table Insumo (
insu_id int primary key not null auto_increment,
insu_nome varchar (50),
insu_marca varchar (50),
insu_valor_compra float,
insu_quantidade int,
insu_tipo varchar (200),
insu_codigo_barra varchar (50),
fk_forn_id int,
foreign key (fk_forn_id) references Fornecedor (forn_id)
);

delimiter $$
create procedure cadastrar_insumo (nome varchar(50), marca varchar(50), compra float, quantidade int, tipo varchar(200), codigo_barra varchar(50), fornecedor_id int) 
begin
    declare mensagem varchar(300);

    if (nome <> '') and (marca <> '') and (nome is not null) and (marca is not null) then
        if (fornecedor_id) is not null then -- Verificar se a FK existe na tabela Fornecedor
            insert into Insumo (insu_nome, insu_marca, insu_valor_compra, insu_quantidade, insu_tipo, insu_codigo_barra, fk_forn_id)
            values (nome, marca, compra, quantidade, tipo, codigo_barra, fornecedor_id);

            set mensagem = 'Insumo inserido com sucesso';
        else
            set mensagem = 'A chave estrangeira não existe na tabela Fornecedor';
        end if;
    else
        set mensagem = 'Os campos nome e marca são obrigatórios';
    end if;

    select mensagem as resultado;
end;
$$ delimiter ;

CALL cadastrar_insumo('Mascaras descartáveis', 'Fênix', 10.5, 100, 'Tipo X', '1234567890', 1);
CALL cadastrar_insumo('Seringa', 'Topfarma', 8.75, 50, 'Tipo Y', '9876543210', 2);
CALL cadastrar_insumo('Luvas descartáveis', 'Inoven', 12.0, 75, 'Tipo Z', '1357924680', 3);

create table Servico (
serv_id int primary key not null auto_increment,
serv_nome varchar (50),
serv_valor_venda float,
serv_duracao time,
serv_tipo varchar (50)
);

delimiter $$
create procedure cadastrar_servico (nome_servico varchar(50), valor_venda float, duracao_servico time, tipo_servico varchar(50)) 
begin
    declare mensagem varchar(300);

    if (nome_servico <> '') and (nome_servico is not null) then
        if (valor_venda > 0) then
            insert into Servico (serv_nome, serv_valor_venda, serv_duracao, serv_tipo)
			values (nome_servico, valor_venda, duracao_servico, tipo_servico);

			set mensagem = 'Serviço inserido com sucesso';
        else
            set mensagem = 'O valor de venda deve ser maior que zero';
        end if;
    else
        set mensagem = 'O campo nome é obrigatório';
    end if;

    select mensagem as resultado;
end ;
$$ delimiter ;

CALL cadastrar_servico('Teste Covid', 50.0, '01:30:00', 'Tipo X');
CALL cadastrar_servico('Exame de Sangue', 75.0, '02:15:00', 'Tipo Y');
CALL cadastrar_servico('Vancina Tétano', 60.0, '01:45:00', 'Tipo Z');

create table Insumo_Servico (
ioso_id int primary key not null auto_increment,
ioso_quantidade_insumo int,
fk_serv_id int,
fk_insu_id int,
foreign key (fk_serv_id) references Servico (serv_id),
foreign key (fk_insu_id) references Insumo (insu_id)
);

delimiter $$
create procedure cadastrar_insumo_servico (insumo_quantidade int, servico_id int, insumo_id int) 
begin
    declare mensagem varchar(300);
    declare fk_serv_id int;
    declare fk_insu_id int;

    set fk_serv_id = (select serv_id from Servico where serv_id = servico_id);
    set fk_insu_id = (select insu_id from Insumo where insu_id = insumo_id);
    
    if (fk_serv_id is not null) and (fk_insu_id) is not null then 
        if (insumo_quantidade > 0) then
            insert into Insumo_Servico (ioso_quantidade_insumo, fk_serv_id, fk_insu_id)
            values (insumo_quantidade, servico_id, insumo_id);

            set mensagem = 'Relação inserida com sucesso';
        else
            set mensagem = 'A quantidade de insumo não pode ser menor ou igual a zero';
        end if;
    else
        set mensagem = 'Uma ou ambas as chaves estrangeiras não existem nas tabelas Servico e Insumo';
    end if;
    
    select mensagem as resultado;
end ; 
$$ delimiter ;

CALL cadastrar_insumo_servico(10, 1, 1);
CALL cadastrar_insumo_servico(5, 2, 2);
CALL cadastrar_insumo_servico(8, 3, 2);

create table Caixa (
caix_id int primary key not null auto_increment,
caix_numero int,
caix_data date,
caix_horario_inicial time,
caix_horario_final time,
caix_status varchar (20),
caix_saldo_inicial float,
caix_saldo_final float,
caix_total_entrada float,
caix_total_saida float,
fk_func_id int,
foreign key (fk_func_id) references Funcionario (func_id)
);

delimiter $$
create procedure cadastrar_caixa (numero_caixa int, data_caixa date, horario_inicial time, horario_final time, status_caixa varchar(20), saldo_inicial float, saldo_final float, total_entrada float, total_saida float, func_id_param int)
begin
    declare mensagem varchar(300);

    if numero_caixa >= 0 then
        if (status_caixa <> '') and (status_caixa is not null) then
            if (func_id_param is not null) then 
                insert into Caixa (caix_numero, caix_data, caix_horario_inicial, caix_horario_final, caix_status, caix_saldo_inicial, caix_saldo_final, caix_total_entrada, caix_total_saida, fk_func_id)
                values (numero_caixa, data_caixa, horario_inicial, horario_final, status_caixa, saldo_inicial, saldo_final, total_entrada, total_saida, func_id_param);

                set mensagem = 'Caixa inserido com sucesso';
            else
                set mensagem = 'A chave estrangeira não existe na tabela Funcionario';
            end if;
        else
            set mensagem = 'Verifique o status esta preenchido';
        end if;
    else
        set mensagem = 'O número do caixa não pode ser negativo';
    end if;

    select mensagem as resultado;
end; 
$$ delimiter ;

CALL cadastrar_caixa(1, '2023-09-20', '08:00:00', '16:00:00', 'Aberto', 1000.00, 1200.00, 3000.00, 2800.00, 1);
CALL cadastrar_caixa(2, '2023-09-21', '09:30:00', '18:30:00', 'Aberto', 1500.00, 1800.00, 3500.00, 3200.00, 2);
CALL cadastrar_caixa(3, '2023-09-22', '07:45:00', '15:45:00', 'Fechado', 1200.00, 1400.00, 2800.00, 2600.00, 3);

create table Despesa (
desp_id int primary key not null auto_increment,
desp_data date,
desp_valor float,
desp_desc varchar (100),
desp_tipo varchar (20),
desp_quantidade_parcelas int
);

delimiter $$
create procedure cadastrar_despesa (data_despesa date, valor_despesa float, descricao_despesa varchar(100), tipo_despesa varchar(20), quantidade_parcelas int) 
begin
    declare mensagem varchar(300);

    if valor_despesa > 0 then
        if descricao_despesa <> '' and (descricao_despesa is not null) then
            insert into Despesa (desp_data, desp_valor, desp_desc, desp_tipo, desp_quantidade_parcelas)
            values (data_despesa, valor_despesa, descricao_despesa, tipo_despesa, quantidade_parcelas);

            set mensagem = 'Despesa inserida com sucesso';
        else
            set mensagem = 'A descrição não está preenchida';
        end if;
    else
        set mensagem = 'O valor da despesa deve ser maior que zero';
    end if;
     select mensagem as resultado;
end; 
$$ delimiter ;

CALL cadastrar_despesa('2023-09-20', 1000.00, 'Aluguel', 'Fixa', 1);
CALL cadastrar_despesa('2023-09-21', 500.00, 'Conta de Luz', 'Fixa', 2);
CALL cadastrar_despesa('2023-09-22', 300.00, 'Material de Escritório', 'Variável', 3);

create table Compra (
comp_id int primary key not null auto_increment,
comp_data date,
comp_valor float,
fk_desp_id int,
foreign key (fk_desp_id) references Despesa (desp_id)
);

delimiter $$
create procedure cadastrar_compra (data_compra date, valor_compra float, despesa_id int)
begin
    declare mensagem varchar(300);

    if (valor_compra <= 0) then
		set mensagem = 'O valor da compra deve ser maior que zero e não pode ser negativo';
	else 
		if  (despesa_id is null) then
			set mensagem = 'Verifique as chaves estrangeiras existem na tabela correspondente';
		else
			insert into Compra (comp_data, comp_valor, fk_desp_id)
			values (data_compra, valor_compra, despesa_id);
			set mensagem = 'Compra inserida com sucesso';
		end if;
    end if;
    
    select mensagem as resultado;
end;
$$ delimiter ;

call cadastrar_compra('2023-09-20', 1000.00, 1);
call cadastrar_compra('2023-09-21', 500.00, 2);
call cadastrar_compra('2023-09-22', 300.00, 3);

create table Pagamento (
paga_id int primary key not null auto_increment,
paga_data date,
paga_valor float,
paga_forma_pagamento varchar(50),
paga_status varchar(20),
paga_vencimento date,
paga_numero_parcela int,
fk_caix_id int,
fk_desp_id int,
foreign key (fk_caix_id) references Caixa (caix_id),
foreign key (fk_desp_id) references Despesa (desp_id)
);

delimiter $$
create procedure cadastrar_pagamento (data_pagamento date, valor_pagamento float, forma_pagamento varchar(50), status_pagamento varchar(20), vencimento_pagamento date, numero_parcela int, caixa_id int, despesa_id int) 
begin
    declare mensagem varchar(300);
    
    if (valor_pagamento > 0) then
        if (caixa_id is not null) and (despesa_id is not null) then
            if (forma_pagamento <> '') and (forma_pagamento is not null) then
                insert into Pagamento (paga_data, paga_valor, paga_forma_pagamento, paga_status, paga_vencimento, paga_numero_parcela, fk_caix_id, fk_desp_id)
                values (data_pagamento, valor_pagamento, forma_pagamento, status_pagamento, vencimento_pagamento, numero_parcela, caixa_id, despesa_id);

                set mensagem = 'Pagamento inserido com sucesso';
            else
                set mensagem = 'O campo forma de pagamento não pode estar vazio';
            end if;
        else
            set mensagem = 'As chaves estrangeiras não existe na tabela correspondente';
        end if;
    else
        set mensagem = 'O valor do pagamento deve ser maior que zero e não pode ser negativo';
    end if;

    select mensagem as resultado;
end;
$$ delimiter ;

call cadastrar_pagamento('2023-09-20', 1000.00, 'Credito', 'Pago', '2023-10-20', 6, 1, 1);
call cadastrar_pagamento('2023-09-21', 500.00, 'Dinheiro', 'Pendente', '2023-10-21', 6, 2, 1);
call cadastrar_pagamento('2023-09-22', 300.00, 'Cheque', 'Pendente', '2023-10-22', 6, 3, 1);

create table Venda(
vend_id int primary key not null auto_increment,
vend_data date,
vend_valor float,
vend_desconto float,
vend_quantidade_parcelas int,
fk_clie_id int,
fk_func_id int,
foreign key (fk_clie_id) references Cliente (clie_id),
foreign key (fk_func_id) references Funcionario (func_id)
);

delimiter $$
create procedure cadastrar_venda (data_venda date, valor_venda float, desconto_venda float, quantidade_parcelas int, cliente_id int, funcionario_id int) 
begin
    declare mensagem varchar(300);

    if (valor_venda > 0) then
        if (cliente_id is not null) and (funcionario_id is not null) then
            insert into Venda (vend_data, vend_valor, vend_desconto, vend_quantidade_parcelas, fk_clie_id, fk_func_id)
            values (data_venda, valor_venda, desconto_venda, quantidade_parcelas, cliente_id, funcionario_id);

            set mensagem = 'Venda inserida com sucesso';
        else
            set mensagem = 'As chaves estrangeiras não existe na tabela correspondente';
        end if;
    else
        set mensagem = 'O valor da venda deve ser maior que zero e não pode ser negativo';
    end if;

    select mensagem as resultado;
end; 
$$ delimiter ;

call cadastrar_venda('2023-09-20', 100, 50.00, 2, 1, 1);
call cadastrar_venda('2023-09-21', 150, 0.00, 2, 2, 2);
call cadastrar_venda('2023-09-22', 200, 25.00, 2, 3, 3);

create table Recebimento(
rece_id int primary key not null auto_increment,
rece_data date,
rece_valor float,
rece_forma_recebimento varchar(50),
rece_status varchar(20),
rece_vencimento date,
rece_numero_parcela int,
fk_caix_id int,
fk_vend_id int,
foreign key (fk_caix_id) references Caixa (caix_id),
foreign key (fk_vend_id) references Venda (vend_id)
);

delimiter $$
create procedure cadastrar_recebimento(data_ven varchar(25), valor float, forma_recebimento varchar(50), status_ven varchar(20), vencimento varchar(25), numero_parcela int, caixa int, venda int)
begin
	if (venda is not null) and (venda > 0) and (venda <= (select max(vend_id) from venda))
	then
		insert into recebimento values (null, data_ven, valor, forma_recebimento, status_ven, vencimento, numero_parcela, caixa, venda);
        select 'Cadastro realizado com sucesso!' as 'Sucesso!';
    else
		select 'A venda selecionada é inválida!' as 'Erro!';
    end if;
end;
$$ delimiter ;

call cadastrar_recebimento('2023-09-20', 50, 'Transferência', 'Recebida', '2023-10-20', 1, 1, 1);
call cadastrar_recebimento('2023-09-21', 75, 'Transferência', 'Recebida', '2023-10-21', 1, 2, 2);
call cadastrar_recebimento('2023-09-22', 100, 'Transferência', 'Recebida', '2023-10-22', 1, 3, 3);

create table Venda_Produto (
vapo_id int primary key not null auto_increment,
vapo_quantidade_item int,
vapo_valor_item float,
fk_prod_id int,
fk_vend_id int,
foreign key (fk_prod_id) references Produto (prod_id),
foreign key (fk_vend_id) references Venda (vend_id)
);

delimiter $$
create procedure cadastrar_venda_produto(quantidade int, valor float, produto int, venda int)
begin
	if (venda is not null) and (venda > 0) and (venda <= (select max(vend_id) from venda))
	then
		insert into venda_produto values (null, quantidade, valor, produto, venda);
        select 'Cadastro realizado com sucesso!' as 'Sucesso!';
    else
		select 'A venda selecionada é inválida!' as 'Erro!';
    end if;
end;
$$ delimiter ;

call cadastrar_venda_produto(6, 200, 1, 1);
call cadastrar_venda_produto(3, 70, 2, 2);
call cadastrar_venda_produto(4, 120, 3, 3);

create table Venda_Medicamento (
vamo_id int primary key not null auto_increment,
vamo_quantidade_item int,
vamo_valor_item float,
fk_medi_id int,
fk_vend_id int,
foreign key (fk_medi_id) references Medicamento (medi_id),
foreign key (fk_vend_id) references Venda (vend_id)
);

delimiter $$
create procedure cadastrar_venda_medicamento(quantidade int, valor float, medicamento int, venda int)
begin
	if (venda is not null) and (venda > 0) and (venda <= (select max(vend_id) from venda))
	then
		insert into venda_medicamento values (null, quantidade, valor, medicamento, venda);
        select 'Cadastro realizado com sucesso!' as 'Sucesso!';
    else
		select 'A venda selecionada é inválida!' as 'Erro!';
    end if;
end;
$$ delimiter ;

call cadastrar_venda_medicamento(2, 99.99, 1, 1);
call cadastrar_venda_medicamento(1, 65, 2, 2);
call cadastrar_venda_medicamento(1, 80, 3, 3);

create table Venda_Servico (
vaso_id int primary key not null auto_increment,
vaso_quantidade_item int,
vaso_valor_servico float,
fk_serv_id int,
fk_vend_id int,
foreign key (fk_serv_id) references Servico (serv_id),
foreign key (fk_vend_id) references Venda (vend_id)
);

delimiter $$
create procedure cadastrar_venda_servico(quantidade int, valor float, servico int, venda int)
begin
	if (venda is not null) and (venda > 0) and (venda <= (select max(vend_id) from venda))
	then
		insert into venda_servico values (null, quantidade, valor, servico, venda);
        select 'Cadastro realizado com sucesso!' as 'Sucesso!';
    else
		select 'A venda selecionada é inválida!' as 'Erro!';
    end if;
end;
$$ delimiter ;

call cadastrar_venda_servico(1, 100, 1, 1);
call cadastrar_venda_servico(1, 200, 2, 2);
call cadastrar_venda_servico(1, 300, 3, 3);

create table Compra_Produto (
capo_id int primary key not null auto_increment,
capo_quantidade_item int,
capo_valor_item float,
fk_comp_id int,
fk_prod_id int,
foreign key (fk_comp_id) references Compra (comp_id),
foreign key (fk_prod_id) references Produto (prod_id)
);

delimiter $$
create procedure cadastrar_compra_produto(quantidade int, valor float, compra int, produto int)
begin
	if (compra is not null) and (compra > 0) and (compra <= (select max(comp_id) from compra))
	then
		insert into compra_produto values (null, quantidade, valor, compra, produto);
        select 'Cadastro realizado com sucesso!' as 'Sucesso!';
    else
		select 'A compra selecionada é inválida!' as 'Erro!';
    end if;
end;
$$ delimiter ;

call cadastrar_compra_produto(30, 300, 1, 1);
call cadastrar_compra_produto(50, 600, 2, 2);
call cadastrar_compra_produto(100, 1200, 3, 3);

create table Compra_Medicamento (
camo_id int primary key not null auto_increment,
camo_quantidade_item int,
campo_valor_item float,
fk_comp_id int,
fk_medi_id int,
foreign key (fk_comp_id) references Compra (comp_id),
foreign key (fk_medi_id) references Medicamento (medi_id)
);

delimiter $$
create procedure cadastrar_compra_medicamento(quantidade int, valor float, compra int, medicamento int)
begin
	if (compra is not null) and (compra > 0) and (compra <= (select max(comp_id) from compra))
	then
		insert into compra_medicamento values (null, quantidade, valor, compra, medicamento);
        select 'Cadastro realizado com sucesso!' as 'Sucesso!';
    else
		select 'A compra selecionada é inválida!' as 'Erro!';
    end if;
end;
$$ delimiter ;

call cadastrar_compra_medicamento(10, 1000, 1, 1);
call cadastrar_compra_medicamento(20, 1500, 2, 2);
call cadastrar_compra_medicamento(30, 2000, 3, 3);

create table Compra_Insumo (
caio_id int primary key not null auto_increment,
caio_quantidade_item int,
caio_valor_item float,
fk_comp_id int,
fk_insu_id int,
foreign key (fk_comp_id) references Compra (comp_id),
foreign key (fk_insu_id) references Insumo (insu_id)
);

delimiter $$
create procedure cadastrar_compra_insumo(quantidade int, valor float, compra int, insumo int)
begin
	if (compra is not null) and (compra > 0) and (compra <= (select max(comp_id) from compra))
	then
		insert into compra_insumo values (null, quantidade, valor, compra, insumo);
        select 'Cadastro realizado com sucesso!' as 'Sucesso!';
    else
		select 'A compra selecionada é inválida!' as 'Erro!';
    end if;
end;
$$ delimiter ;

call cadastrar_compra_insumo(70, 2200, 1, 1);
call cadastrar_compra_insumo(50, 1600, 2, 2);
call cadastrar_compra_insumo(30, 1200, 3, 3);

create table Prescricao (
pres_id int primary key not null auto_increment,
pres_data date,
pres_patologia varchar(50),
pres_vencimento date,
pres_nome_emissor varchar(50),
pres_clinica_emissora varchar(50),
pres_scan_documento blob,
fk_clie_id int,
fk_medi_id int,
fk_func_id int,
fk_vend_id int,
foreign key (fk_clie_id) references Cliente (clie_id),
foreign key (fk_medi_id) references Medicamento (medi_id),
foreign key (fk_func_id) references Funcionario (func_id),
foreign key (fk_vend_id) references Venda (vend_id)
);

delimiter $$
create procedure cadastrar_prescricao (data_pre varchar(25), patologia varchar (50), vencimento varchar(25), nome_emissor varchar(50), clinica_emissora varchar(50), cliente int, medicamento int, funcionario int, venda int)
begin
	if (cliente is not null) and (cliente > 0) and (cliente <= (select max(clie_id) from cliente))
	then
		insert into prescricao values (null, data_pre, patologia, vencimento, nome_emissor, clinica_emissora, null, cliente, medicamento, funcionario, venda);
        select 'Cadastro realizado com sucesso!' as 'Sucesso!';
    else
		select 'O cliente selecionado é inválido!' as 'Erro!';
    end if;
end;
$$ delimiter ;

call cadastrar_prescricao('2022-01-01', 'Alergia', '2022-02-01', 'Dra. Giulia Novais', 'Novais Saúde', 1, 1, 1, 1);
call cadastrar_prescricao('2022-01-02', 'Asma', '2022-02-02', 'Dr. Jackson Henrique', 'Sql Health', 2, 2, 2, 2);
call cadastrar_prescricao('2022-01-03', 'Coronavírus', '2022-02-03', 'Dr. IFRO', 'Ifro Clinic', 3, 3, 3, 3);