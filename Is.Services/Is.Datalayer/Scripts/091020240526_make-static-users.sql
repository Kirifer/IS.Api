do $$
begin
  -- insert static users
  insert into users (id, username, password, first_name, last_name, email, is_admin) values (gen_random_uuid(), 'engagement', '@itsquarehub00033', 'Allan', 'Espiritu', 'allan.espiritu@itsquarehub.com', true);
end;
$$