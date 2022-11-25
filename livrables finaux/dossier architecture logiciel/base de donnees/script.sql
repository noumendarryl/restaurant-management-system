/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2012                    */
/* Created on:     25/11/2022 12:31:17                          */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('realize') and o.name = 'FK_REALIZE_REALIZE_ORDER')
alter table realize
   drop constraint FK_REALIZE_REALIZE_ORDER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('realize') and o.name = 'FK_REALIZE_REALIZE2_SUPPLIER')
alter table realize
   drop constraint FK_REALIZE_REALIZE2_SUPPLIER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('stock_com') and o.name = 'FK_STOCK_CO_STOCK_COM_ORDER')
alter table stock_com
   drop constraint FK_STOCK_CO_STOCK_COM_ORDER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('stock_com') and o.name = 'FK_STOCK_CO_STOCK_COM_COMMODIT')
alter table stock_com
   drop constraint FK_STOCK_CO_STOCK_COM_COMMODIT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('stock_mat') and o.name = 'FK_STOCK_MA_STOCK_MAT_ORDER')
alter table stock_mat
   drop constraint FK_STOCK_MA_STOCK_MAT_ORDER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('stock_mat') and o.name = 'FK_STOCK_MA_STOCK_MAT_MATERIAL')
alter table stock_mat
   drop constraint FK_STOCK_MA_STOCK_MAT_MATERIAL
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Commodities')
            and   type = 'U')
   drop table Commodities
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Materials')
            and   type = 'U')
   drop table Materials
go

if exists (select 1
            from  sysobjects
           where  id = object_id('"Order"')
            and   type = 'U')
   drop table "Order"
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Supplier')
            and   type = 'U')
   drop table Supplier
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('realize')
            and   name  = 'realize2_FK'
            and   indid > 0
            and   indid < 255)
   drop index realize.realize2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('realize')
            and   name  = 'realize_FK'
            and   indid > 0
            and   indid < 255)
   drop index realize.realize_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('realize')
            and   type = 'U')
   drop table realize
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('stock_com')
            and   name  = 'stock_com2_FK'
            and   indid > 0
            and   indid < 255)
   drop index stock_com.stock_com2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('stock_com')
            and   name  = 'stock_com_FK'
            and   indid > 0
            and   indid < 255)
   drop index stock_com.stock_com_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('stock_com')
            and   type = 'U')
   drop table stock_com
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('stock_mat')
            and   name  = 'stock_mat2_FK'
            and   indid > 0
            and   indid < 255)
   drop index stock_mat.stock_mat2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('stock_mat')
            and   name  = 'stock_mat_FK'
            and   indid > 0
            and   indid < 255)
   drop index stock_mat.stock_mat_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('stock_mat')
            and   type = 'U')
   drop table stock_mat
go

/*==============================================================*/
/* Table: Commodities                                           */
/*==============================================================*/
create table Commodities (
   id_com               numeric              identity,
   designation_com      varchar(200)         null,
   storage_limit_time   datetime             null,
   constraint PK_COMMODITIES primary key nonclustered (id_com)
)
go

/*==============================================================*/
/* Table: Materials                                             */
/*==============================================================*/
create table Materials (
   id_mat               numeric              identity,
   designation_mat      varchar(200)         null,
   constraint PK_MATERIALS primary key nonclustered (id_mat)
)
go

/*==============================================================*/
/* Table: "Order"                                               */
/*==============================================================*/
create table "Order" (
   id_ord               numeric              identity,
   title                varchar(200)         null,
   order_date           datetime             null,
   delivery_date        datetime             null,
   constraint PK_ORDER primary key nonclustered (id_ord)
)
go

/*==============================================================*/
/* Table: Supplier                                              */
/*==============================================================*/
create table Supplier (
   id_sup               numeric              identity,
   name                 varchar(200)         null,
   location             varchar(200)         null,
   constraint PK_SUPPLIER primary key nonclustered (id_sup)
)
go

/*==============================================================*/
/* Table: realize                                               */
/*==============================================================*/
create table realize (
   id_ord               numeric              not null,
   id_sup               numeric              not null,
   constraint PK_REALIZE primary key (id_ord, id_sup)
)
go

/*==============================================================*/
/* Index: realize_FK                                            */
/*==============================================================*/
create index realize_FK on realize (
id_ord ASC
)
go

/*==============================================================*/
/* Index: realize2_FK                                           */
/*==============================================================*/
create index realize2_FK on realize (
id_sup ASC
)
go

/*==============================================================*/
/* Table: stock_com                                             */
/*==============================================================*/
create table stock_com (
   id_ord               numeric              not null,
   id_com               numeric              not null,
   price                float                null,
   initial_quantity     int                  null,
   remaining_quantity   int                  null,
   constraint PK_STOCK_COM primary key (id_ord, id_com)
)
go

/*==============================================================*/
/* Index: stock_com_FK                                          */
/*==============================================================*/
create index stock_com_FK on stock_com (
id_ord ASC
)
go

/*==============================================================*/
/* Index: stock_com2_FK                                         */
/*==============================================================*/
create index stock_com2_FK on stock_com (
id_com ASC
)
go

/*==============================================================*/
/* Table: stock_mat                                             */
/*==============================================================*/
create table stock_mat (
   id_ord               numeric              not null,
   id_mat               numeric              not null,
   price                float                null,
   initial_quantity     int                  null,
   remaining_quantity   int                  null,
   constraint PK_STOCK_MAT primary key (id_ord, id_mat)
)
go

/*==============================================================*/
/* Index: stock_mat_FK                                          */
/*==============================================================*/
create index stock_mat_FK on stock_mat (
id_ord ASC
)
go

/*==============================================================*/
/* Index: stock_mat2_FK                                         */
/*==============================================================*/
create index stock_mat2_FK on stock_mat (
id_mat ASC
)
go

alter table realize
   add constraint FK_REALIZE_REALIZE_ORDER foreign key (id_ord)
      references "Order" (id_ord)
go

alter table realize
   add constraint FK_REALIZE_REALIZE2_SUPPLIER foreign key (id_sup)
      references Supplier (id_sup)
go

alter table stock_com
   add constraint FK_STOCK_CO_STOCK_COM_ORDER foreign key (id_ord)
      references "Order" (id_ord)
go

alter table stock_com
   add constraint FK_STOCK_CO_STOCK_COM_COMMODIT foreign key (id_com)
      references Commodities (id_com)
go

alter table stock_mat
   add constraint FK_STOCK_MA_STOCK_MAT_ORDER foreign key (id_ord)
      references "Order" (id_ord)
go

alter table stock_mat
   add constraint FK_STOCK_MA_STOCK_MAT_MATERIAL foreign key (id_mat)
      references Materials (id_mat)
go

