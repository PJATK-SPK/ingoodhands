truncate table core.stocks;
truncate table core.delivery_products, core.deliveries, core.order_products, core.orders;
truncate table core.notifications;
truncate table core.donation_products, core.donations;
update core.counters set value = 1;

-- 100 helmow dla PL001, 45kg ryzu, 80l wody, 12 winter jacket
INSERT INTO core.stocks(product_id, quantity, update_user_id, updated_at, status, warehouse_id) VALUES(20, 100, 1, '2023-05-07 10:11:23.261', 0, 1);
INSERT INTO core.stocks(product_id, quantity, update_user_id, updated_at, status, warehouse_id) VALUES(1, 45, 1, '2023-05-07 10:11:23.261', 0, 1);
INSERT INTO core.stocks(product_id, quantity, update_user_id, updated_at, status, warehouse_id) VALUES(8, 80, 1, '2023-05-07 10:11:23.261', 0, 1);
INSERT INTO core.stocks(product_id, quantity, update_user_id, updated_at, status, warehouse_id) VALUES(24, 12, 1, '2023-05-07 10:11:23.261', 0, 1);

-- 350 racji zyw. dla PL002
INSERT INTO core.stocks(product_id, quantity, update_user_id, updated_at, status, warehouse_id) VALUES(22, 350, 1, '2023-05-07 12:13:39.503', 0, 2);