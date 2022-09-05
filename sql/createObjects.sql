-- Database: myserver

-- DROP DATABASE IF EXISTS myserver;

CREATE DATABASE myserver
    WITH
    OWNER = postgres
ENCODING = 'UTF8'
    LC_COLLATE = 'Portuguese_Brazil.1252'
    LC_CTYPE = 'Portuguese_Brazil.1252'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;


-- Table: public.list

-- DROP TABLE IF EXISTS public.list;

CREATE TABLE
IF NOT EXISTS public.list
(
    cod_task integer NOT NULL,
    description_task character varying
(40) COLLATE pg_catalog."default" NOT NULL,
    status_task character varying
(2) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT list_pkey PRIMARY KEY
(cod_task)
)

TABLESPACE pg_default;

ALTER TABLE
IF EXISTS public.list
    OWNER to postgres;