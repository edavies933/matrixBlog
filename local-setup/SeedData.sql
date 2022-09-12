
INSERT INTO tags(id,name) values(
 '1ba85f64-5717-4562-b3fc-2c963f66afa1'::uuid,
 'Education'
);

INSERT INTO tags(id,name) values(
 '2fa85f64-5717-4562-b3fc-2c963f66afa7'::uuid,
 'Health and fitness'
);

INSERT INTO tags(id,name) values(
 '3fa85f64-5717-4562-b3fc-2c963f66afa8'::uuid,
 'Fashion'
);

INSERT INTO tags(id,name) values(
 '4fa85f64-5717-4562-b3fc-2c963f66afa9'::uuid,
 'Programing'
);

INSERT INTO blogs(id,user_id,created_date,last_modified_date,name,description) values(
 '1fa85f64-5717-4562-b3fc-2c963f66afa9'::uuid,'11a85f64-5717-4562-b3fc-2c963f66afa9'::uuid,
 current_timestamp,current_timestamp,'First blog','first blog discription details'
);

INSERT INTO blogs(id,user_id,created_date,last_modified_date,name,description) values(
 '2fa85f64-5717-4562-b3fc-2c963f66afa9'::uuid,'11a85f64-5717-4562-b3fc-2c963f66afa9'::uuid,
 current_timestamp,current_timestamp,'Second blog','Second blog discription details'
);

INSERT INTO blog_posts(id,user_id,blog_id,created_date,last_modified_date,name,content) values(
 '1ca85f64-5717-4562-b3fc-2c963f66afa9'::uuid,'11a85f64-5717-4562-b3fc-2c963f66afa9'::uuid,'1fa85f64-5717-4562-b3fc-2c963f66afa9'::uuid,
 current_timestamp,current_timestamp,'blog 1 post 1','Sfff discription details'
);

INSERT INTO blog_posts(id,user_id,blog_id,created_date,last_modified_date,name,content) values(
 '2ca85f64-5717-4562-b3fc-2c963f66afa9'::uuid,'11a85f64-5717-4562-b3fc-2c963f66afa9'::uuid,'1fa85f64-5717-4562-b3fc-2c963f66afa9'::uuid,
  current_timestamp,current_timestamp,'blog 1 post 2','Second blog discription details'
);

INSERT INTO blog_posts(id,user_id,blog_id,created_date,last_modified_date,name,content) values(
 '3ca85f64-5717-4562-b3fc-2c963f66afa9'::uuid,'11a85f64-5717-4562-b3fc-2c963f66afa9'::uuid,'2fa85f64-5717-4562-b3fc-2c963f66afa9'::uuid,
 current_timestamp,current_timestamp,'blog 2 post 1','Second blog discription details'
);

INSERT INTO comments(id,user_id,blog_post_id,content,created_date,last_modified_date) values(
 '1da85f64-5717-4562-b3fc-2c963f66afa9'::uuid,'11a85f64-5717-4562-b3fc-2c963f66afa9'::uuid,'1ca85f64-5717-4562-b3fc-2c963f66afa9'::uuid,
 'coment 1 for blog 1',current_timestamp,current_timestamp
);
INSERT INTO comments(id,user_id,blog_post_id,content,created_date,last_modified_date) values(
 '2da85f64-5717-4562-b3fc-2c963f66afa9'::uuid,'11a85f64-5717-4562-b3fc-2c963f66afa9'::uuid,'1ca85f64-5717-4562-b3fc-2c963f66afa9'::uuid,
 'coment  2for blog 1',current_timestamp,current_timestamp
);

INSERT INTO comments(id,user_id,blog_post_id,content,created_date,last_modified_date) values(
 '3da85f64-5717-4562-b3fc-2c963f66afa9'::uuid,'11a85f64-5717-4562-b3fc-2c963f66afa9'::uuid,'2ca85f64-5717-4562-b3fc-2c963f66afa9'::uuid,
 'coment 1 for blog 2',current_timestamp,current_timestamp
);
INSERT INTO comments(id,user_id,blog_post_id,content,created_date,last_modified_date) values(
 '4da85f64-5717-4562-b3fc-2c963f66afa9'::uuid,'11a85f64-5717-4562-b3fc-2c963f66afa9'::uuid,'2ca85f64-5717-4562-b3fc-2c963f66afa9'::uuid,
 'coment  2for blog 2',current_timestamp,current_timestamp
);