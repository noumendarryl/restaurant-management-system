/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2012                    */
/* Created on:     29/11/2022 12:20:42                          */
/*==============================================================*/

create database AppRestaurant
use AppRestaurant
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Etapes') and o.name = 'FK_ETAPES_POSSEDE_RECETTE')
alter table Etapes
   drop constraint FK_ETAPES_POSSEDE_RECETTE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('Stock') and o.name = 'FK_STOCK_APPARTIEN_INGREDIE')
alter table Stock
   drop constraint FK_STOCK_APPARTIEN_INGREDIE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('associe') and o.name = 'FK_ASSOCIE_ASSOCIE_RECETTE')
alter table associe
   drop constraint FK_ASSOCIE_ASSOCIE_RECETTE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('associe') and o.name = 'FK_ASSOCIE_ASSOCIE2_COMMANDE')
alter table associe
   drop constraint FK_ASSOCIE_ASSOCIE2_COMMANDE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('necessite') and o.name = 'FK_NECESSIT_NECESSITE_MATERIEL')
alter table necessite
   drop constraint FK_NECESSIT_NECESSITE_MATERIEL
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('necessite') and o.name = 'FK_NECESSIT_NECESSITE_RECETTE')
alter table necessite
   drop constraint FK_NECESSIT_NECESSITE_RECETTE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('utilise') and o.name = 'FK_UTILISE_UTILISE_INGREDIE')
alter table utilise
   drop constraint FK_UTILISE_UTILISE_INGREDIE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('utilise') and o.name = 'FK_UTILISE_UTILISE2_RECETTE')
alter table utilise
   drop constraint FK_UTILISE_UTILISE2_RECETTE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Commande')
            and   type = 'U')
   drop table Commande
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Etapes')
            and   name  = 'possede_FK'
            and   indid > 0
            and   indid < 255)
   drop index Etapes.possede_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Etapes')
            and   type = 'U')
   drop table Etapes
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Ingredients')
            and   type = 'U')
   drop table Ingredients
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Materiels')
            and   type = 'U')
   drop table Materiels
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Recette')
            and   type = 'U')
   drop table Recette
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ReservationTable')
            and   type = 'U')
   drop table ReservationTable
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('Stock')
            and   name  = 'appartient_FK'
            and   indid > 0
            and   indid < 255)
   drop index Stock.appartient_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Stock')
            and   type = 'U')
   drop table Stock
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('associe')
            and   name  = 'associe2_FK'
            and   indid > 0
            and   indid < 255)
   drop index associe.associe2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('associe')
            and   name  = 'associe_FK'
            and   indid > 0
            and   indid < 255)
   drop index associe.associe_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('associe')
            and   type = 'U')
   drop table associe
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('necessite')
            and   name  = 'necessite2_FK'
            and   indid > 0
            and   indid < 255)
   drop index necessite.necessite2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('necessite')
            and   name  = 'necessite_FK'
            and   indid > 0
            and   indid < 255)
   drop index necessite.necessite_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('necessite')
            and   type = 'U')
   drop table necessite
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('utilise')
            and   name  = 'utilise2_FK'
            and   indid > 0
            and   indid < 255)
   drop index utilise.utilise2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('utilise')
            and   name  = 'utilise_FK'
            and   indid > 0
            and   indid < 255)
   drop index utilise.utilise_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('utilise')
            and   type = 'U')
   drop table utilise
go

/*==============================================================*/
/* Table: Commande                                              */
/*==============================================================*/
create table Commande (
   id_commande          int              identity,
   num_table                varchar(200)      not null,
   nb_commandes         int                not null,
   prix                 double                  not null,
   constraint PK_COMMANDE primary key nonclustered (id_commande)
);
go

/*==============================================================*/
/* Table: Etapes                                                */
/*==============================================================*/
create table Etapes (
   id_etape             int              identity,
   id_recette           int              not null,
   nb_etape             int                  not null,
   intitule          varchar(200)         not null,
   duree                int                  not null,
   nom_materiel    varchar(200)         not null,
   constraint PK_ETAPES primary key nonclustered (id_etape)
);
insert into Etapes(id_recette, nb_etape, description, duree) values 
(1, 1, 'Préchauffer le four à 230° (th 7-8)', 1), (1, 2, 'Mélanger la chair de crabe, le jus de citron, la chapelure, les herbes et le piment', 1),
(1, 4, 'Saler et poivrer', 1), (1, 7, 'Badigeonner les feuilletés avec un oeuf battu et salé, puis strier avec une fourchette', 1),
(3, 1, 'Enduire la cocotte de saindoux', 1), (3, 3, 'couvrir d’une couche de pommes de terre tranchées', 1), (3, 4, 'Mettre à nouveau une couche de pommes de terre, ail, sel, poivre et 1 piment oiseau ou autre', 1), 
(9, 1, 'Etaler la pate dans un moule', 1), (9, 2, 'Faire fondre le beurrer', 1), (9, 6, 'Egoutter le thon et les champignons puis les ajouter à la béchamelle', 1), 
(9, 7, 'Mettre appareil sur la pate et parsemer de gruyère', 1), (9, 8, 'Mettre au four 20mn', 1);
go

/*==============================================================*/
/* Index: possede_FK                                            */
/*==============================================================*/
create index possede_FK on Etapes (
id_recette ASC
);
go

/*==============================================================*/
/* Table: Ingredients                                           */
/*==============================================================*/
create table Ingredients (
   id_ingredient        int              identity,
   nom                  varchar(200)         not null,
   quantite             int                  not null,
   constraint PK_INGREDIENTS primary key nonclustered (id_ingredient)
);
insert into Ingredients(nom, quantite) values
('Oeufs', 2), ('Creme fraiche', 4), ('Echalotes', 2), ('Jus de citron', 1), ('Soupe de percil hache', 3), ('Sachet de gruyere rape', 1),
('Chair a saucisse', 3), ('Boeuf hache', 1), ('Concentre de tomate', 1), ('Verre de farine', 1), ('Cubes de bouillon de boeuf', 2), 
('Lait', 1), ('Boite de thon', 1), ('Boite de champignons de paris', 1), ('Baies', 5), ('Bol de langues oiseaux', 1), ('Bouquet de coriande', 1), 
('Foie gras', 1), ('Tomates', 4), ('Gousses ail', 4), ('Echalotes', 4), ('Branche de fenouil sauvage', 1), ('Verre vinaigre blanc', 1), ('Botte persil plat', 1);
go

/*==============================================================*/
/* Table: Materiels                                             */
/*==============================================================*/
create table Materiels (
   id_materiel          int              identity,
   nom                  varchar(200)        not null,
   quantite             int                  not null,
   type                 varchar(200)       not null,
   constraint PK_MATERIELS primary key nonclustered (id_materiel)
);
insert into Materiels(nom, quantite, type) values
('Tables de 2 personnes', 10, 'commun'), ('Tables de 4 personnes', 10, 'commun'), ('Tables de 6 personnes', 5, 'commun'),
('Tables de 8 personnes', 5, 'commun'), ('Tables de 10 personnes', 2, 'commun'), ('Petites assiettes', 150, 'commun'),
('Assiettes plates', 150, 'commun'), ('Assiettes creuses', 30, 'commun'), ('Fourchettes', 150, 'commun'), ('Couteaux', 150, 'commun'),
('Cuilleres a soupe', 150, 'commun'), ('Cuilleres a cafe', 150, 'commun'), ('Verres a eau', 150, 'commun'), ('Verres a vin', 150, 'commun'),
('Flutes a champagne', 150, 'commun'), ('Jeux de tasses et assiettes a cafe', 50, 'commun'), ('Serviettes en tissu', 150, 'commun'), 
('Nappes', 40, 'commun'), ('Feux de cuisson', 5, 'cuisine'), ('Casseroles', 10, 'cuisine'), ('Four', 1, 'cuisine'), ('Cuillere en bois', 10, 'cuisne');
go

/*==============================================================*/
/* Table: Recette                                               */
/*==============================================================*/
create table Recette (
   id_recette           int              identity,
   nom                  varchar(200)         not null,
   nb_personnes         int                  not null,
   categorie            varchar(200)       not null,
   prix                 double              not null,
   constraint PK_RECETTE primary key nonclustered (id_recette)
);
insert Recette(nom, nb_personnes, categorie, prix) values
('Feuillete de crabe', 4, 'Entree', 200), ('Oeufs cocotte', 4, 'Entree', 150),
('Bouillinade anguilles ou poissons', 4, 'Plat', 80.30), ('Boles de picoulats', 25, 'Plat', 100),
('Blanquette de veau', 5, 'Plat', 105), ('Taglitelles de concombre fume', 4, 'Entree', 180), 
('Tiramisu', 4, 'Dessert', 75.99), ('Chorba', 4, 'Entree', 90.50), ('Tarte au thon', 6, 'Entree', 250);
go

/*==============================================================*/
/* Table: ReservationTable                                      */
/*==============================================================*/
create table ReservationTable (
   id_reservation       int              identity,
   nom_client                varchar(200)        not  null,
   nb_personnes         int                  not null,
   horaire              datetime            not null,
   constraint PK_RESERVATIONTABLE primary key nonclustered (id_reservation)
);
insert into ReservationTable(nom_client, nb_personnes, horaire) values
('Madeleine', 4, '2022-12-01 12:15:00'), ('Barry', 2, '2023-01-09 20:00:00'), ('Andrea', 10, '2022-12-25 10:50:00'),
('Vincent', 2, '2023-02-14 14:05:20'), ('Adrien', 6, '2023-03-08 19:20:50'), ('Marie', 8, '2023-04-07 21:30:00');
go

/*==============================================================*/
/* Table: Stock                                                 */
/*==============================================================*/
create table Stock (
   id_stock             int              identity,
   id_ingredient        int              not null,
   nom                  varchar(200)      not null,
   quantite             int         not null,
   categorie            varchar(200)      not null,
   constraint PK_STOCK primary key nonclustered (id_stock)
);
insert into Stock(id_ingredient, nom, quantite, categorie) values
(1, 'Oeufs', 50, 'produitLDC'), (3, 'Citron', 20, 'produitLDC'), (19, 'Tomates', 50, 'produitLDC'),
(8, 'Boeufs haches', 10, 'produitFrais'), (7, 'Chair a saucisse', 30, 'produitFrais'), (13, 'Echalotes', 50, 'produitLDC'),
(21, 'Boites de thon', 50, 'produitLDC'), (2, 'Creme fraiche', 50, 'produitSurgeles'), (18, 'Foie gras', 40, 'produitFrais');
go

/*==============================================================*/
/* Index: appartient_FK                                         */
/*==============================================================*/
create index appartient_FK on Stock (
id_ingredient ASC
);
go

/*==============================================================*/
/* Table: associe                                               */
/*==============================================================*/
create table associe (
   id_recette           int              not null,
   id_commande          int              not null,
   constraint PK_ASSOCIE primary key (id_recette, id_commande)
);
insert into associe(id_recette, id_commande) values
();
go

/*==============================================================*/
/* Index: associe_FK                                            */
/*==============================================================*/
create index associe_FK on associe (
id_recette ASC
);
go

/*==============================================================*/
/* Index: associe2_FK                                           */
/*==============================================================*/
create index associe2_FK on associe (
id_commande ASC
);
go

/*==============================================================*/
/* Table: necessite                                             */
/*==============================================================*/
create table necessite (
   id_materiel          int              not null,
   id_recette           int              not null,
   constraint PK_NECESSITE primary key (id_materiel, id_recette)
);
insert into necessite(id_materiel, id_recette) values 
(21, 1);
go

/*==============================================================*/
/* Index: necessite_FK                                          */
/*==============================================================*/
create index necessite_FK on necessite (
id_materiel ASC
);
go

/*==============================================================*/
/* Index: necessite2_FK                                         */
/*==============================================================*/
create index necessite2_FK on necessite (
id_recette ASC
);
go

/*==============================================================*/
/* Table: utilise                                               */
/*==============================================================*/
create table utilise (
   id_ingredient        int              not null,
   id_recette           int              not null,
   constraint PK_UTILISE primary key (id_ingredient, id_recette)
);
insert into utilise(id_ingredient, id_recette) values 
();
go

/*==============================================================*/
/* Index: utilise_FK                                            */
/*==============================================================*/
create index utilise_FK on utilise (
id_ingredient ASC
);
go

/*==============================================================*/
/* Index: utilise2_FK                                           */
/*==============================================================*/
create index utilise2_FK on utilise (
id_recette ASC
);
go

alter table Etapes
   add constraint FK_ETAPES_POSSEDE_RECETTE foreign key (id_recette)
      references Recette (id_recette)
go

alter table Stock
   add constraint FK_STOCK_APPARTIEN_INGREDIE foreign key (id_ingredient)
      references Ingredients (id_ingredient)
go

alter table associe
   add constraint FK_ASSOCIE_ASSOCIE_RECETTE foreign key (id_recette)
      references Recette (id_recette)
go

alter table associe
   add constraint FK_ASSOCIE_ASSOCIE2_COMMANDE foreign key (id_commande)
      references Commande (id_commande)
go

alter table necessite
   add constraint FK_NECESSIT_NECESSITE_MATERIEL foreign key (id_materiel)
      references Materiels (id_materiel)
go

alter table necessite
   add constraint FK_NECESSIT_NECESSITE_RECETTE foreign key (id_recette)
      references Recette (id_recette)
go

alter table utilise
   add constraint FK_UTILISE_UTILISE_INGREDIE foreign key (id_ingredient)
      references Ingredients (id_ingredient)
go

alter table utilise
   add constraint FK_UTILISE_UTILISE2_RECETTE foreign key (id_recette)
      references Recette (id_recette)
go

