do $$
begin
  -- insert static users
  insert into users (id, username, password, first_name, last_name, email, is_admin) values (gen_random_uuid(), 'admin', 'password', 'Patrick', 'Perez', 'patric@gmail.com', true);
end;
$$