do $$
begin
  -- Create supply codes table
  create table if not exists public.supply_codes (
    id uuid not null,
    code text not null,
    sequence_code int not null,
    category text not null,
    item text not null,
    color text not null,
    size text not null,
    quantity int not null,
    supply_taken boolean default false,

    constraint pk_supplycodes primary key (id)

   
  );

  create index ix_supply_codes_id
    on public.supply_codes using btree
    (id asc nulls last);
end;
$$