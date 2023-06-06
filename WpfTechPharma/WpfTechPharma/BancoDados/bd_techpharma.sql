#created by Henrique Xavier

create database bd_techpharma;
use bd_techpharma;

create table Endereco(
ende_id int primary key not null,
ende_estado varchar (200),
ende_cidade varchar (50),
ende_bairro varchar (50),
ende_rua varchar (50),
ende_numero int,
ende_complemento varchar (200),
ende_cep varchar (20)
);

create table Cliente (
clie_id int primary key not null,
clie_nome varchar (50),
clie_sexo varchar (20),
clie_nascimento date,
clie_rg varchar (25),
clie_cpf varchar (25),
clie_email varchar (50),
clie_contato varchar (20),
id_ende_fk int,
foreign key (id_ende_fk) references Endereco (ende_id)
);

create table Funcionario (
func_id int primary key not null,
func_nome varchar (50),
func_sexo varchar (20),
func_nascimento date,
func_rg varchar (20),
func_cpf varchar (20),
func_email varchar (50),
func_contato varchar (20),
func_funcao varchar(50),
func_salario float,
id_ende_fk int,
foreign key (id_ende_fk) references Endereco (ende_id)
);

create table Usuario (
usua_id int primary key not null,
usua_login varchar (20),
usua_senha varchar (20),
id_func_fk int,
foreign key (id_func_fk) references Funcionario (func_id)
);

create table Caixa (
caix_id int primary key not null,
caix_numero int,
caix_data date,
caix_horario_inicial time,
caix_horario_final time,
caix_status varchar (20),
caix_saldo_inicial float,
caix_saldo_final float,
caix_total_entrada float,
caix_total_saida float
);

create table Despesa (
desp_id int primary key not null,
desp_data date,
desp_valor float,
desp_desc varchar (100),
desp_tipo varchar (20),
desp_quantidade_parcelas int
);

create table Venda(
vend_id int primary key not null,
vend_data date,
vend_valor float,
vend_desconto float,
vend_quantidade_parcelas int,
id_clie_fk int,
id_func_fk int,
foreign key (id_clie_fk) references Cliente (clie_id),
foreign key (id_func_fk) references Funcionario (func_id)
);

create table Servico (
serv_id int primary key not null,
serv_nome varchar (50),
serv_valor_venda float,
serv_duracao time,
serv_tipo varchar (50)
);

create table Venda_Servico (
vaso_id int,
vaso_quantidade_item int,
vaso_valor_servico float,

);

create table Fornecedor (
forn_id int primary key not null,
forn_razao_social varchar (100),
forn_nome_fantasia varchar (100),
forn_email varchar (50),
forn_contato varchar (20),
forn_inscricao_estudal varchar (50),
id_ende_fk int,
foreign key (id_ende_fk) references Endereco (ende_id)
);

create table Insumo (
insu_id int primary key not null,
insu_nome varchar (50),
insumo_marca varchar (50),
insumo_valor_compra float,
insu_quantidade int,
insu_tipo varchar (200),
insu_codigo_barra varchar (50),
id_forn_fk int,
foreign key (id_insu_fk) references Fornecedor (forn_id)
);

create table Produto (
prod_id int primary key not null,
prod_nome varchar (50),
prod_marca varchar (50),
prod_valor_compra float,
prod_valor_vend float,
prod_quantidade int,
prod_tipo varchar (50),
prod_codigo_barra varchar (50),
id_forn_fk int,
foreign key (id_insu_fk) references Fornecedor (forn_id)
);

create table Medicamento (
medi_id int primary key not null,
medi_nome varchar (50),
medi_marca varchar (20),
medi_peso_volume varchar (10),
medi_valor_compra float,
medi_valor_venda float,
medi_quantidade int,
medi_tarja varchar (20),
medi_codigo_barra varchar (50),
id_forn_fk int,
foreign key (id_forn_fk) references Fornecedor (forn_id)
);

create table Venda_Produto (
vapo_id int primary key not null,
vapo_quantidade_item int,
vapo_valor_item float,
id_prod_fk int,
id_vend_fk int,
foreign key (id_prod_fk) references Produto (prod_id),
foreign key (id_vend_fk) references Venda (vend_id)
);

create table Compra (
comp_id int primary key not null,
comp_data date,
comp_valor float,
id_forn_fk int,
id_desp_fk int,
foreign key (id_forn_fk) references Fornecedor (forn_id),
foreign key (id_desp_fk) references Despesa (desp_id)
);

create table Compra_Insumo (
caio_id int,
caio_quantidade_item int,
caio_valor_item float,
id_comp_fk int,
id_insu_fk int,
foreign key (id_comp_fk) references Compra (comp_id),
foreign key (id_insu_fk) references Insumo (insu_id)
);

create table Compra_Produto (
capo_id int,
capo_quantidade_item int,
capo_valor_item float,
id_comp_fk int,
id_prod_fk int,
foreign key (id_comp_fk) references Compra (comp_id),
foreign key (id_prod_fk) references Produto (prod_id)
);

create table Compra_Medicamento (
camo_id int,
camo_quantidade_item int,
campo_valor_item float,
id_comp_fk int,
id_medi_fk int,
foreign key (id_comp_fk) references Compra (comp_id),
foreign key (id_medi_fk) references Medicamento (medi_id)
);

