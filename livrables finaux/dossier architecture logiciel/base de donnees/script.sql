/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2012                    */
/* Created on:     23/11/2022 10:50:23                          */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('contain') and o.name = 'FK_CONTAIN_CONTAIN_ORDER')
alter table contain
   drop constraint FK_CONTAIN_CONTAIN_ORDER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('contain') and o.name = 'FK_CONTAIN_CONTAIN2_MATERIAL')
alter table contain
   drop constraint FK_CONTAIN_CONTAIN2_MATERIAL
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('contain') and o.name = 'FK_CONTAIN_CONTAIN3_COMMODIT')
alter table contain
   drop constraint FK_CONTAIN_CONTAIN3_COMMODIT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('realize') and o.name = 'FK_REALIZE_REALIZE_SUPPLIER')
alter table realize
   drop constraint FK_REALIZE_REALIZE_SUPPLIER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('realize') and o.name = 'FK_REALIZE_REALIZE2_ORDER')
alter table realize
   drop constraint FK_REALIZE_REALIZE2_ORDER
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
           where  id    = object_id('contain')
            and   name  = 'contain3_FK'
            and   indid > 0
            and   indid < 255)
   drop index contain.contain3_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('contain')
            and   name  = 'contain2_FK'
            and   indid > 0
            and   indid < 255)
   drop index contain.contain2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('contain')
            and   name  = 'contain_FK'
            and   indid > 0
            and   indid < 255)
   drop index contain.contain_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('contain')
            and   type = 'U')
   drop table contain
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

/*==============================================================*/
/* Table: Commodities                                           */
/*==============================================================*/
create table Commodities (
   id_com               int                  not null,
   name                 varchar(20)          null,
   initial_quantity     int                  null,
   remaining_quantity   int                  null,
   state                bit                  null,
   constraint PK_COMMODITIES primary key nonclustered (id_com)
)
go

/*==============================================================*/
/* Table: Materials                                             */
/*==============================================================*/
create table Materials (
   id_mat               int                  not null,
   name                 varchar(20)          null,
   initial_quantity     int                  null,
   remaining_quantity   int                  null,
   constraint PK_MATERIALS primary key nonclustered (id_mat)
)
go

/*==============================================================*/
/* Table: "Order"                                               */
/*==============================================================*/
create table "Order" (
   id_ord               numeric              identity,
   name                 varchar(20)          null,
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
   name                 varchar(20)          null,
   location             varchar(50)          null,
   constraint PK_SUPPLIER primary key nonclustered (id_sup)
)
go

/*==============================================================*/
/* Table: contain                                               */
/*==============================================================*/
create table contain (
   id_ord               numeric              not null,
   id_mat               int                  not null,
   id_com               int                  not null,
   price                float                null,
   constraint PK_CONTAIN primary key (id_ord, id_mat, id_com)
)
go

/*==============================================================*/
/* Index: contain_FK                                            */
/*==============================================================*/
create index contain_FK on contain (
id_ord ASC
)
go

/*==============================================================*/
/* Index: contain2_FK                                           */
/*==============================================================*/
create index contain2_FK on contain (
id_mat ASC
)
go

/*==============================================================*/
/* Index: contain3_FK                                           */
/*==============================================================*/
create index contain3_FK on contain (
id_com ASC
)
go

/*==============================================================*/
/* Table: realize                                               */
/*==============================================================*/
create table realize (
   id_sup               numeric              not null,
   id_ord               numeric              not null,
   constraint PK_REALIZE primary key (id_sup, id_ord)
)
go

/*==============================================================*/
/* Index: realize_FK                                            */
/*==============================================================*/
create index realize_FK on realize (
id_sup ASC
)
go

/*==============================================================*/
/* Index: realize2_FK                                           */
/*==============================================================*/
create index realize2_FK on realize (
id_ord ASC
)
go

alter table contain
   add constraint FK_CONTAIN_CONTAIN_ORDER foreign key (id_ord)
      references "Order" (id_ord)
go

alter table contain
   add constraint FK_CONTAIN_CONTAIN2_MATERIAL foreign key (id_mat)
      references Materials (id_mat)
go

alter table contain
   add constraint FK_CONTAIN_CONTAIN3_COMMODIT foreign key (id_com)
      references Commodities (id_com)
go

alter table realize
   add constraint FK_REALIZE_REALIZE_SUPPLIER foreign key (id_sup)
      references Supplier (id_sup)
go

alter table realize
   add constraint FK_REALIZE_REALIZE2_ORDER foreign key (id_ord)
      references "Order" (id_ord)
go

