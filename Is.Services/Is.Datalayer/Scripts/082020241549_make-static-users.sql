do $$
begin
  -- insert static users
  insert into users (id, username, password, first_name, last_name, email, is_admin) values (gen_random_uuid(), 'admin', 'password', 'Admin', 'Test', 'jam.payumo@itsquarehub.com', true);
end;
$$