do $$
begin
  -- Create users table
  create table if not exists public.supplies (
    id uuid not null,
    category text not null,
    item text not null,
    color text not null,
    size text not null,
    quantity int not null,
    supplies_taken int not null,
    supplies_left int not null,
    cost_per_unit decimal not null,
    total decimal not null,

    constraint pk_supplies primary key (id)

   
  );

  create index ix_supplies_id
    on public.supplies using btree
    (id asc nulls last);
end;
$$