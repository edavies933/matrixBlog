CREATE TABLE tags (
    id uuid NOT NULL,
    name varchar NOT NULL,
    PRIMARY KEY (Id)
);

create table blogs (
    id uuid not null,
	user_id uuid not null,
    created_date timestamp  null,
	last_modified_date timestamp  null,
    name varchar not null,
	description varchar not null,
    primary key (id)
);

create table blog_posts (
    id uuid not null,
	user_id uuid not null,
	blog_id uuid not null,
    content varchar not null,
    created_date timestamp  null,
	last_modified_date timestamp  null,
    name varchar not null,
    primary key (id)
);

create table comments (
    id uuid not null,
	user_id uuid not null,
	blog_post_id uuid not null,
    content varchar not null,
    created_date timestamp  null,
	last_modified_date timestamp  null,
    primary key (id)
);

create table blog_post_tag ( 
    id uuid not null,
	blog_post_id uuid not null,
	tag_id uuid not null,
    primary key (id)
);