\connect Gamedb

CREATE TABLE public."GameMaps"
(
    "GameMapId" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "Map" text COLLATE pg_catalog."default" NOT NULL,
    "Width" integer,
    "Height" integer,
    "NoObstacles" integer,
    "CreatedTime" timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    "IsActive" boolean DEFAULT true,
    "Map1" json,
    CONSTRAINT "PK_GameMapId" PRIMARY KEY ("GameMapId")
);

CREATE TABLE public."TankModels"
(
    "TankModelId" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "TankModelName" text COLLATE pg_catalog."default" NOT NULL,
    "TankModelDescription" text COLLATE pg_catalog."default",
    "Speed" integer NOT NULL,
    "GunRange" integer NOT NULL,
    "GunPower" double precision NOT NULL,
    "ShieldLife" double precision NOT NULL,
    "CreatedTime" timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    "IsActive" boolean DEFAULT true,
    CONSTRAINT "PK_TankModelId" PRIMARY KEY ("TankModelId")
);

CREATE TABLE public."GameBattles"
(
    "GameBattleId" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "BlueTankModelId" integer NOT NULL,
    "BlueTankX" integer,
    "BlueTankY" integer,
    "RedTankModelId" integer NOT NULL,
    "RedTankX" integer,
    "RedTankY" integer,
    "GameMapId" integer NOT NULL,
    "CreatedTime" timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    "IsActive" boolean DEFAULT true,
    CONSTRAINT "PK_GameBattleId" PRIMARY KEY ("GameBattleId")
);


CREATE TABLE public."GameSimulations"
(
    "GameSimulationId" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "GameBattleId" integer NOT NULL,
    "Simulation" text COLLATE pg_catalog."default",
    "CreatedTime" timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    "IsActive" boolean DEFAULT true,
    CONSTRAINT "PK_GameSimulationId" PRIMARY KEY ("GameSimulationId")
);