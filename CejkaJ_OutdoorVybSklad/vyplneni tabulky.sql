-- Naplneni tabulky Zbozi
INSERT INTO dbo.Zbozi (Nazev, Popis, PocetKsSklad, CenaKs) 
VALUES 
('Cepín', 'Lezecký cepín pro náročné horolezce', 50, 1500),
('Mačky', 'Mačky pro alpské lezení', 40, 2000),
('Lano', 'Dynamické lezecké lano, 70m', 30, 3000),
('Karabiny', 'Lezecké karabiny s pojistkou', 100, 500),
('Sedák', 'Sedák pro sportovní lezení, velikost M', 60, 1200);
-- Naplneni tabulky Zak
INSERT INTO dbo.Zak (Jmeno, Prijmeni, Adresa) 
VALUES
('Jan', 'Novák', 'Praha, Ulice 1'),
('Petr', 'Svoboda', 'Brno, Ulice 2'),
('Eva', 'Müllerová', 'Ostrava, Ulice 3');
-- Naplneni tabulky Prodejka
INSERT INTO dbo.Prodejka (IdZak, Cena, DatumVytv) 
VALUES
(1, 5500, '2025-03-13'), -- Prodejka pro Jana Nováka
(2, 3500, '2025-03-14'), -- Prodejka pro Petra Svobodu
(3, 4700, '2025-03-15'); -- Prodejka pro Evu Müllerovou
-- Naplneni tabulky ZboNM
INSERT INTO dbo.ZboNM (IdZbo, IdProd, Pocet)
VALUES 
(1, 4, 2), -- 2 cepíny pro prodejku 1 (Jan Novák)
(2, 4, 1), -- 1 mačky pro prodejku 1 (Jan Novák)
(3, 5, 1), -- 1 lano pro prodejku 2 (Petr Svoboda)
(4, 5, 4), -- 4 karabiny pro prodejku 2 (Petr Svoboda)
(5, 3, 1); -- 1 sedák pro prodejku 3 (Eva Müllerová)
