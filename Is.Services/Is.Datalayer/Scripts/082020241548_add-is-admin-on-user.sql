do $$
begin
  -- alter users table and add column is_admin
  alter table if exists public.users add column if not exists is_admin boolean default false;
end;
$$