--Insert Categorias
INSERT INTO CATEGORIA (
    NMCATEGORIA,
    DSCATEGORIA
) VALUES(
    'Backend', 
    'backend'
);

INSERT INTO CATEGORIA (
    NMCATEGORIA,
    DSCATEGORIA
) VALUES(
    'Frontend', 
    'frontend'
);

INSERT INTO CATEGORIA (
    NMCATEGORIA,
    DSCATEGORIA
) VALUES(
    'fullstack', 
    'Full Stack'
);

INSERT INTO CATEGORIA (
    NMCATEGORIA,
    DSCATEGORIA
) VALUES(
    'Mobile', 
    'mobile'
);

--Insert funções

INSERT INTO FUNCAO (
    NMFUNCAO,
    DSFUNCAO
) VALUES (
    'Usuario',
    'Usuário'
);

INSERT INTO FUNCAO (
    NMFUNCAO,
    DSFUNCAO
) VALUES (
    'Autor',
    'Autor'
);

INSERT INTO FUNCAO (
    NMFUNCAO,
    DSFUNCAO
) VALUES (
    'Admin',
    'Administrador'
);

--Insert Tags

INSERT INTO TAG(
    NMTAG,
    DSTAG
) VALUES(
    'ASP.NET',
    'asp.net'
);

INSERT INTO TAG(
    NMTAG,
    DSTAG
) VALUES(
    '.NET',
    'dotnet'
);

INSERT INTO TAG(
    NMTAG,
    DSTAG
) VALUES(
    'C#',
    'csharp'
);

INSERT INTO TAG(
    NMTAG,
    DSTAG
) VALUES(
    'Angular',
    'angular'
);

INSERT INTO TAG(
    NMTAG,
    DSTAG
) VALUES(
    'Flutter',
    'flutter'
);

INSERT INTO TAG(
    NMTAG,
    DSTAG
) VALUES(
    'Entity Framework',
    'ef'
);

INSERT INTO TAG(
    NMTAG,
    DSTAG
) VALUES(
    'Java',
    'java'
);

INSERT INTO TAG(
    NMTAG,
    DSTAG
) VALUES(
    'Javascript',
    'javascript'
);

COMMIT;