#created by Henrique Xavier

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

create table Fornecedor (
forn_id int primary key not null auto_increment,
forn_razao_social varchar (100),
forn_nome_fantasia varchar (100),
forn_cnpj varchar (20),
forn_email varchar (50),
forn_contato varchar (20),
forn_inscricao_estadual varchar (50),
fk_ende_id int,
foreign key (fk_ende_id) references Endereco (ende_id)
);

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

create table Usuario (
usua_id int primary key not null auto_increment,
usua_login varchar (20),
usua_senha varchar (20),
fk_func_id int,
foreign key (fk_func_id) references Funcionario (func_id)
);

create table Produto (
prod_id int primary key not null auto_increment,
prod_nome varchar (50),
prod_marca varchar (50),
prod_valor_compra float,
prod_valor_vend float,
prod_quantidade int,
prod_tipo varchar (50),
prod_codigo_barra varchar (50),
fk_forn_id int,
foreign key (fk_forn_id) references Fornecedor (forn_id)
);

create table Medicamento (
medi_id int primary key not null auto_increment,
medi_nome varchar (50),
medi_marca varchar (20),
medi_peso_volume varchar (10),
medi_valor_compra float,
medi_valor_venda float,
medi_quantidade int,
medi_tarja varchar (20),
medi_codigo_barra varchar (50),
fk_forn_id int,
foreign key (fk_forn_id) references Fornecedor (forn_id)
);

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

create table Servico (
serv_id int primary key not null auto_increment,
serv_nome varchar (50),
serv_valor_venda float,
serv_duracao varchar (50),
serv_tipo varchar (50),
fk_insu_id int,
foreign key (fk_insu_id) references Insumo (insu_id)
);

create table Insumo_Servico (
ioso_id int primary key not null auto_increment,
ioso_quantidade_insumo int,
fk_serv_id int,
fk_insu_id int,
foreign key (fk_serv_id) references Servico (serv_id),
foreign key (fk_insu_id) references Insumo (insu_id)
);

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

create table Despesa (
desp_id int primary key not null auto_increment,
desp_data date,
desp_valor float,
desp_desc varchar (100),
desp_tipo varchar (20),
desp_quantidade_parcelas int
);

create table Compra (
comp_id int primary key not null auto_increment,
comp_data date,
comp_valor float,
fk_forn_id int,
fk_desp_id int,
foreign key (fk_forn_id) references Fornecedor (forn_id),
foreign key (fk_desp_id) references Despesa (desp_id)
);

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

create table Venda_Produto (
vapo_id int primary key not null auto_increment,
vapo_quantidade_item int,
vapo_valor_item float,
fk_prod_id int,
fk_vend_id int,
foreign key (fk_prod_id) references Produto (prod_id),
foreign key (fk_vend_id) references Venda (vend_id)
);

create table Venda_Medicamento (
vamo_id int primary key not null auto_increment,
vamo_quantidade_item int,
vamo_valor_item float,
fk_medi_id int,
fk_vend_id int,
foreign key (fk_medi_id) references Medicamento (medi_id),
foreign key (fk_vend_id) references Venda (vend_id)
);

create table Venda_Servico (
vaso_id int primary key not null auto_increment,
vaso_quantidade_item int,
vaso_valor_servico float,
fk_serv_id int,
fk_vend_id int,
foreign key (fk_serv_id) references Servico (serv_id),
foreign key (fk_vend_id) references Venda (vend_id)
);

create table Compra_Produto (
capo_id int primary key not null auto_increment,
capo_quantidade_item int,
capo_valor_item float,
fk_comp_id int,
fk_prod_id int,
foreign key (fk_comp_id) references Compra (comp_id),
foreign key (fk_prod_id) references Produto (prod_id)
);

create table Compra_Medicamento (
camo_id int primary key not null auto_increment,
camo_quantidade_item int,
campo_valor_item float,
fk_comp_id int,
fk_medi_id int,
foreign key (fk_comp_id) references Compra (comp_id),
foreign key (fk_medi_id) references Medicamento (medi_id)
);

create table Compra_Insumo (
caio_id int primary key not null auto_increment,
caio_quantidade_item int,
caio_valor_item float,
fk_comp_id int,
fk_insu_id int,
foreign key (fk_comp_id) references Compra (comp_id),
foreign key (fk_insu_id) references Insumo (insu_id)
);

create table Prescricao (
pres_id int primary key not null auto_increment,
pres_data date,
pres_patologia varchar(50),
pres_vendimento date,
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