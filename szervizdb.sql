-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2026. Ápr 23. 12:22
-- Kiszolgáló verziója: 10.4.32-MariaDB
-- PHP verzió: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `szervizdb`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `munkalapok`
--

CREATE TABLE `munkalapok` (
  `MunkalapID` int(11) NOT NULL,
  `Rendszam` varchar(15) NOT NULL,
  `Hiba_leirasa` text NOT NULL,
  `Allapot` varchar(50) DEFAULT 'Folyamatban',
  `Rogzites_datuma` datetime DEFAULT current_timestamp(),
  `UgyfelID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `munkalapok`
--

INSERT INTO `munkalapok` (`MunkalapID`, `Rendszam`, `Hiba_leirasa`, `Allapot`, `Rogzites_datuma`, `UgyfelID`) VALUES
(1, 'ABC-123', 'Fékbetét csere, olajcsere', 'Kész', '2026-04-13 12:47:10', 1),
(2, 'XYZ-987', 'Kopogó hang a futómű felől', 'Folyamatban', '2026-04-13 12:47:10', 2),
(4, 'TE SZ-123', 'teszt', 'Alkatrészre vár', '2026-04-13 12:59:15', 2),
(6, 'AA AA-123', 'asdasdasd', 'Alkatrészre vár', '2026-04-16 09:58:34', 5);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `ugyfelek`
--

CREATE TABLE `ugyfelek` (
  `UgyfelID` int(11) NOT NULL,
  `Nev` varchar(100) NOT NULL,
  `Telefonszam` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `ugyfelek`
--

INSERT INTO `ugyfelek` (`UgyfelID`, `Nev`, `Telefonszam`) VALUES
(1, 'Kovács János', '+36 30 123 4567'),
(2, 'Nagy Éva', '+36 20 987 6543'),
(3, 'Hunti', 'pape'),
(4, 'fa', 'fa'),
(5, 'teszt2', 'adfs'),
(6, 'asdf', '+36 (1) 123 4567'),
(7, 'asd', '06705167109');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `munkalapok`
--
ALTER TABLE `munkalapok`
  ADD PRIMARY KEY (`MunkalapID`),
  ADD KEY `UgyfelID` (`UgyfelID`);

--
-- A tábla indexei `ugyfelek`
--
ALTER TABLE `ugyfelek`
  ADD PRIMARY KEY (`UgyfelID`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `munkalapok`
--
ALTER TABLE `munkalapok`
  MODIFY `MunkalapID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT a táblához `ugyfelek`
--
ALTER TABLE `ugyfelek`
  MODIFY `UgyfelID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `munkalapok`
--
ALTER TABLE `munkalapok`
  ADD CONSTRAINT `munkalapok_ibfk_1` FOREIGN KEY (`UgyfelID`) REFERENCES `ugyfelek` (`UgyfelID`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
