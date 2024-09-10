do $$
begin
  -- Create budge table
  create table if not exists public.budget_expenses (
    id uuid not null,
    month_created text not null,
    year_created int not null,
    budget decimal not null,
    expenses decimal not null,
    total_budget decimal not null,
    user_id uuid not null,

    constraint pk_budget_expenses primary key (id)

   
  );

  create index ix_budget_expenses_id
    on public.budget_expenses using btree
    (id asc nulls last);
end;
$$