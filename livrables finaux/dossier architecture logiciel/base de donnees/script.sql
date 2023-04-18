/*==============================================================*/
/* DBMS NAME:      Microsoft SQL Server 2012                    */
/* CREATEd on:     29/11/2022 12:20:42                          */
/*==============================================================*/

CREATE DATABASE Tastyl
go

USE Tastyl;
go

IF EXISTS (SELECT 1
   FROM sys.sysreferences r join sys.sysobjects o on (o.id = r.constid AND o.type = 'F')
   WHERE r.fkeyid = object_id('Etapes') AND o.NAME = 'FK_ETAPES_POSSEDE_RECETTE')
ALTER TABLE Etapes
   DROP CONSTRAINT FK_ETAPES_POSSEDE_RECETTE
go

IF EXISTS (SELECT 1
   FROM sys.sysreferences r join sys.sysobjects o on (o.id = r.constid AND o.type = 'F')
   WHERE r.fkeyid = object_id('Stock') AND o.NAME = 'FK_STOCK_APPARTIEN_INGREDIE')
ALTER TABLE Stock
   DROP CONSTRAINT FK_STOCK_APPARTIEN_INGREDIE
go

IF EXISTS (SELECT 1
   FROM sys.sysreferences r join sys.sysobjects o on (o.id = r.constid AND o.type = 'F')
   WHERE r.fkeyid = object_id('associe') AND o.NAME = 'FK_ASSOCIE_ASSOCIE_RECETTE')
ALTER TABLE associe
   DROP CONSTRAINT FK_ASSOCIE_ASSOCIE_RECETTE
go

IF EXISTS (SELECT 1
   FROM sys.sysreferences r join sys.sysobjects o on (o.id = r.constid AND o.type = 'F')
   WHERE r.fkeyid = object_id('associe') AND o.NAME = 'FK_ASSOCIE_ASSOCIE2_COMMANDE')
ALTER TABLE associe
   DROP CONSTRAINT FK_ASSOCIE_ASSOCIE2_COMMANDE
go

IF EXISTS (SELECT 1
   FROM sys.sysreferences r join sys.sysobjects o on (o.id = r.constid AND o.type = 'F')
   WHERE r.fkeyid = object_id('necessite') AND o.NAME = 'FK_NECESSIT_NECESSITE_MATERIEL')
ALTER TABLE necessite
   DROP CONSTRAINT FK_NECESSIT_NECESSITE_MATERIEL
go

IF EXISTS (SELECT 1
   FROM sys.sysreferences r join sys.sysobjects o on (o.id = r.constid AND o.type = 'F')
   WHERE r.fkeyid = object_id('necessite') AND o.NAME = 'FK_NECESSIT_NECESSITE_RECETTE')
ALTER TABLE necessite
   DROP CONSTRAINT FK_NECESSIT_NECESSITE_RECETTE
go

IF EXISTS (SELECT 1
   FROM sys.sysreferences r join sys.sysobjects o on (o.id = r.constid AND o.type = 'F')
   WHERE r.fkeyid = object_id('utilise') AND o.NAME = 'FK_UTILISE_UTILISE_INGREDIE')
ALTER TABLE utilise
   DROP CONSTRAINT FK_UTILISE_UTILISE_INGREDIE
go

IF EXISTS (SELECT 1
   FROM sys.sysreferences r join sys.sysobjects o on (o.id = r.constid AND o.type = 'F')
   WHERE r.fkeyid = object_id('utilise') AND o.NAME = 'FK_UTILISE_UTILISE2_RECETTE')
ALTER TABLE utilise
   DROP CONSTRAINT FK_UTILISE_UTILISE2_RECETTE
go

IF EXISTS (SELECT 1
            FROM  sysobjects
           WHERE  id = object_id('Commande')
            AND   type = 'U')
   DROP table CommANDe
go

IF EXISTS (SELECT 1
            FROM  sysindexes
           WHERE  id    = object_id('Etapes')
            AND   NAME  = 'possede_FK'
            AND   indid > 0
            AND   indid < 255)
   DROP INDEX Etapes.possede_FK
go

IF EXISTS (SELECT 1
            FROM  sysobjects
           WHERE  id = object_id('Etapes')
            AND   type = 'U')
   DROP table Etapes
go

IF EXISTS (SELECT 1
            FROM  sysobjects
           WHERE  id = object_id('Ingredients')
            AND   type = 'U')
   DROP table Ingredients
go

IF EXISTS (SELECT 1
            FROM  sysobjects
           WHERE  id = object_id('Materiels')
            AND   type = 'U')
   DROP table Materiels
go

IF EXISTS (SELECT 1
            FROM  sysobjects
           WHERE  id = object_id('Recette')
            AND   type = 'U')
   DROP table Recette
go

IF EXISTS (SELECT 1
            FROM  sysobjects
           WHERE  id = object_id('ReservationTable')
            AND   type = 'U')
   DROP table ReservationTable
go

IF EXISTS (SELECT 1
            FROM  sysindexes
           WHERE  id    = object_id('Stock')
            AND   NAME  = 'appartient_FK'
            AND   indid > 0
            AND   indid < 255)
   DROP INDEX Stock.appartient_FK
go

IF EXISTS (SELECT 1
            FROM  sysobjects
           WHERE  id = object_id('Stock')
            AND   type = 'U')
   DROP table Stock
go

IF EXISTS (SELECT 1
            FROM  sysindexes
           WHERE  id    = object_id('associe')
            AND   NAME  = 'associe2_FK'
            AND   indid > 0
            AND   indid < 255)
   DROP INDEX associe.associe2_FK
go

IF EXISTS (SELECT 1
            FROM  sysindexes
           WHERE  id    = object_id('associe')
            AND   NAME  = 'associe_FK'
            AND   indid > 0
            AND   indid < 255)
   DROP INDEX associe.associe_FK
go

IF EXISTS (SELECT 1
            FROM  sysobjects
           WHERE  id = object_id('associe')
            AND   type = 'U')
   DROP table associe
go

IF EXISTS (SELECT 1
            FROM  sysindexes
           WHERE  id    = object_id('necessite')
            AND   NAME  = 'necessite2_FK'
            AND   indid > 0
            AND   indid < 255)
   DROP INDEX necessite.necessite2_FK
go

IF EXISTS (SELECT 1
            FROM  sysindexes
           WHERE  id    = object_id('necessite')
            AND   NAME  = 'necessite_FK'
            AND   indid > 0
            AND   indid < 255)
   DROP INDEX necessite.necessite_FK
go

IF EXISTS (SELECT 1
            FROM  sysobjects
           WHERE  id = object_id('necessite')
            AND   type = 'U')
   DROP table necessite
go

IF EXISTS (SELECT 1
            FROM  sysindexes
           WHERE  id    = object_id('utilise')
            AND   NAME  = 'utilise2_FK'
            AND   indid > 0
            AND   indid < 255)
   DROP INDEX utilise.utilise2_FK
go

IF EXISTS (SELECT 1
            FROM  sysindexes
           WHERE  id    = object_id('utilise')
            AND   NAME  = 'utilise_FK'
            AND   indid > 0
            AND   indid < 255)
   DROP INDEX utilise.utilise_FK
go

IF EXISTS (SELECT 1
            FROM  sysobjects
           WHERE  id = object_id('utilise')
            AND   type = 'U')
   DROP table utilise
go

/*==============================================================*/
/* Table: Commande                                              */
/*==============================================================*/
CREATE TABLE Commande (
   id_commande          INT           IDENTITY,
   titre                          VARCHAR(200)        NOT NULL,
   num_table                INT          NOT NULL,
   nb_commandes         INT             NOT NULL,
   prix                FLOAT(2)                  NOT NULL,
   CONSTRAINT PK_COMMANDE PRIMARY KEY NONCLUSTERED (id_commande)
);
INSERT INTO Commande(titre, num_table, nb_commandes, prix) VALUES
('Feuillete de crabe', 1, 5, 1000), ('Blanquette de veau', 6, 2, 310); 
go

/*==============================================================*/
/* Table: Etapes                                                */
/*==============================================================*/
CREATE TABLE Etapes (
   id_etape             INT           IDENTITY,
   id_recette           INT           NOT NULL,
   nb_etape            INT           NOT NULL,
   intitule          VARCHAR(200)         NOT NULL,
   duree                INT               NOT NULL,
   CONSTRAINT PK_ETAPES PRIMARY KEY NONCLUSTERED (id_etape)
);
INSERT INTO Etapes(id_recette, nb_etape, intitule, duree) VALUES 
(1, 1, 'Préchauffer le four à 230° (th 7-8)', 1), (1, 2, 'Mélanger la chair de crabe, le jus de citron, la chapelure, les herbes et le piment', 1),
(1, 4, 'Saler et poivrer', 1), (1, 7, 'Badigeonner les feuilletés avec un oeuf battu et salé, puis strier avec une fourchette', 1),
(3, 1, 'Enduire la cocotte de saindoux', 1), (3, 3, 'couvrir d’une couche de pommes de terre tranchées', 1), (3, 4, 'Mettre à nouveau une couche de pommes de terre, ail, sel, poivre et 1 piment oiseau ou autre', 1), 
(9, 1, 'Etaler la pate dans un moule', 1), (9, 2, 'Faire fondre le beurrer', 1), (9, 6, 'Egoutter le thon et les champignons puis les ajouter à la béchamelle', 1), 
(9, 7, 'Mettre appareil sur la pate et parsemer de gruyère', 1), (9, 8, 'Mettre au four 20mn', 1);
go

/*==============================================================*/
/* INDEX: possede_FK                                            */
/*==============================================================*/
CREATE INDEX possede_FK ON Etapes (
id_recette ASC
);
go

/*==============================================================*/
/* Table: Ingredients                                           */
/*==============================================================*/
CREATE TABLE Ingredients (
   id_ingredient        INT           IDENTITY,
   nom                  VARCHAR(200)         NOT NULL,
   quantite             INT               NOT NULL,
   CONSTRAINT PK_INGREDIENTS PRIMARY KEY NONCLUSTERED (id_ingredient)
);
INSERT INTO Ingredients(nom, quantite) VALUES
('Oeufs', 2), ('Creme fraiche', 4), ('Echalotes', 2), ('Jus de citron', 1), ('Soupe de percil hache', 3), ('Sachet de gruyere rape', 1),
('Chair a saucisse', 3), ('Boeuf hache', 1), ('Concentre de tomate', 1), ('Verre de farine', 1), ('Cubes de bouillon de boeuf', 2), 
('Lait', 1), ('Boite de thon', 1), ('Boite de champignons de paris', 1), ('Baies', 5), ('Bol de langues oiseaux', 1), ('Bouquet de coriANDe', 1), 
('Foie gras', 1), ('Tomates', 4), ('Gousses ail', 4), ('Echalotes', 4), ('Branche de fenouil sauvage', 1), ('Verre vinaigre blanc', 1), ('Botte persil plat', 1);
go

/*==============================================================*/
/* Table: Materiels                                             */
/*==============================================================*/
CREATE TABLE Materiels (
   id_materiel          INT           IDENTITY,
   nom                  VARCHAR(200)        NOT NULL,
   quantite             INT               NOT NULL,
   type                 VARCHAR(200)       NOT NULL,
   CONSTRAINT PK_MATERIELS PRIMARY KEY NONCLUSTERED (id_materiel)
);
INSERT INTO Materiels(nom, quantite, type) VALUES
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
CREATE TABLE Recette (
   id_recette           INT           IDENTITY,
   nom                  VARCHAR(200)         NOT NULL,
   temps_cuisson       INT                   NULL,
   temps_repos          INT                NULL,
   nb_personnes         INT               NOT NULL,
   categorie            VARCHAR(200)       NOT NULL,
   prix                 FLOAT(2)              NOT NULL,
   CONSTRAINT PK_RECETTE PRIMARY KEY NONCLUSTERED (id_recette)
);
INSERT Recette(nom, temps_cuisson, temps_repos, nb_personnes, categorie, prix) VALUES
('Feuillete de crabe', 10, 0, 4, 'Entree', 200), ('Oeufs cocotte', 5, 3, 4, 'Entree', 150),
('Bouillinade anguilles ou poissons', 20, 6, 4, 'Plat', 80.30), ('Boles de picoulats', 0, 0, 25, 'Plat', 100),
('Blanquette de veau', 120, 0, 5, 'Plat', 105), ('Taglitelles de concombre fume', , 5, 4, 'Entree', 180), 
('Tiramisu', 0, 60, 4, 'Dessert', 75.99), ('Chorba', 18, , 4, 'Entree', 90.50), ('Tarte au thon', 45, 5, 6, 'Entree', 250);
go

/*==============================================================*/
/* Table: ReservationTable                                      */
/*==============================================================*/
CREATE TABLE ReservationTable (
   id_reservation       INT           IDENTITY,
   nom_client                VARCHAR(200)        not  null,
   nb_personnes         INT               NOT NULL,
   horaire              datetime            NOT NULL,
   CONSTRAINT PK_RESERVATIONTABLE PRIMARY KEY NONCLUSTERED (id_reservation)
);
INSERT INTO ReservationTable(nom_client, nb_personnes, horaire) VALUES
('Kate Parkin', 4, '2022-12-01 12:15:00'), ('Barry Allen', 2, '2023-01-09 20:00:00'), ('Andrea Collins', 10, '2022-12-25 10:50:00'),
('Vincent Dupont', 2, '2023-02-14 14:05:20'), ('Adrien Agreste', 6, '2023-03-08 19:20:50'), ('Marie Camille', 8, '2023-04-07 21:30:00');
go

/*==============================================================*/
/* Table: Stock                                                 */
/*==============================================================*/
CREATE TABLE Stock (
   id_stock             INT           IDENTITY,
   id_ingredient        INT           NOT NULL,
   nom                  VARCHAR(200)      NOT NULL,
   quantite             INT      NOT NULL,
   categorie            VARCHAR(200)      NOT NULL,
   CONSTRAINT PK_STOCK PRIMARY KEY NONCLUSTERED (id_stock)
);
INSERT INTO Stock(id_ingredient, nom, quantite, categorie) VALUES
(1, 'Oeufs', 50, 'produitLDC'), (3, 'Citron', 20, 'produitLDC'), (19, 'Tomates', 50, 'produitLDC'),
(8, 'Boeufs haches', 10, 'produitFrais'), (7, 'Chair a saucisse', 30, 'produitFrais'), (13, 'Echalotes', 50, 'produitLDC'),
(21, 'Boites de thon', 50, 'produitLDC'), (2, 'Creme fraiche', 50, 'produitSurgeles'), (18, 'Foie gras', 40, 'produitFrais');
go

/*==============================================================*/
/* INDEX: appartient_FK                                         */
/*==============================================================*/
CREATE INDEX appartient_FK ON Stock (
id_ingredient ASC
);
go

/*==============================================================*/
/* Table: associe                                               */
/*==============================================================*/
CREATE TABLE associe (
   id_recette           INT           NOT NULL,
   id_commande          INT           NOT NULL,
   CONSTRAINT PK_ASSOCIE PRIMARY KEY (id_recette, id_commande)
);
INSERT INTO associe(id_recette, id_commande) VALUES
();
go

/*==============================================================*/
/* INDEX: associe_FK                                            */
/*==============================================================*/
CREATE INDEX associe_FK ON associe (
id_recette ASC
);
go

/*==============================================================*/
/* INDEX: associe2_FK                                           */
/*==============================================================*/
CREATE INDEX associe2_FK ON associe (
id_commande ASC
);
go

/*==============================================================*/
/* Table: necessite                                             */
/*==============================================================*/
CREATE TABLE necessite (
   id_materiel          INT           NOT NULL,
   id_recette           INT           NOT NULL,
   CONSTRAINT PK_NECESSITE PRIMARY KEY (id_materiel, id_recette)
);
INSERT INTO necessite(id_materiel, id_recette) VALUES 
(21, 1);
go

/*==============================================================*/
/* INDEX: necessite_FK                                          */
/*==============================================================*/
CREATE INDEX necessite_FK ON necessite (
id_materiel ASC
);
go

/*==============================================================*/
/* INDEX: necessite2_FK                                         */
/*==============================================================*/
CREATE INDEX necessite2_FK ON necessite (
id_recette ASC
);
go

/*==============================================================*/
/* Table: utilise                                               */
/*==============================================================*/
CREATE TABLE utilise (
   id_ingredient        INT           NOT NULL,
   id_recette           INT           NOT NULL,
   CONSTRAINT PK_UTILISE PRIMARY KEY (id_ingredient, id_recette)
);
INSERT INTO utilise(id_ingredient, id_recette) VALUES 
();
go

/*==============================================================*/
/* INDEX: utilise_FK                                            */
/*==============================================================*/
CREATE INDEX utilise_FK ON utilise (
id_ingredient ASC
);
go

/*==============================================================*/
/* INDEX: utilise2_FK                                           */
/*==============================================================*/
CREATE INDEX utilise2_FK ON utilise (
id_recette ASC
);
go

ALTER TABLE Etapes
   ADD CONSTRAINT FK_ETAPES_POSSEDE_RECETTE FOREIGN KEY (id_recette)
      REFERENCES Recette (id_recette);
go

ALTER TABLE Stock
   ADD CONSTRAINT FK_STOCK_APPARTIEN_INGREDIE FOREIGN KEY (id_ingredient)
      REFERENCES Ingredients (id_ingredient);
go

ALTER TABLE associe
   ADD CONSTRAINT FK_ASSOCIE_ASSOCIE_RECETTE FOREIGN KEY (id_recette)
      REFERENCES Recette (id_recette);
go

ALTER TABLE associe
   ADD CONSTRAINT FK_ASSOCIE_ASSOCIE2_COMMANDE FOREIGN KEY (id_commande)
      REFERENCES id_commande (id_commande);
go

ALTER TABLE necessite
   ADD CONSTRAINT FK_NECESSIT_NECESSITE_MATERIEL FOREIGN KEY (id_materiel)
      REFERENCES Materiels (id_materiel);
go

ALTER TABLE necessite
   ADD CONSTRAINT FK_NECESSIT_NECESSITE_RECETTE FOREIGN KEY (id_recette)
      REFERENCES Recette (id_recette);
go

ALTER TABLE utilise
   ADD CONSTRAINT FK_UTILISE_UTILISE_INGREDIE FOREIGN KEY (id_ingredient)
      REFERENCES Ingredients (id_ingredient);
go

ALTER TABLE utilise
   ADD CONSTRAINT FK_UTILISE_UTILISE2_RECETTE FOREIGN KEY (id_recette)
      REFERENCES Recette (id_recette);
go

