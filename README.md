# Unity projekts - UI elementu izmantošana Unity dzinī

Unity projekts izstrādāts balstoties uz LTPC ieskaties definētajiem uzdevumiem. Projektam veltītas 9h laika (github repository izveidots agrāk)

Projekta ietaros ticis izvirzīts koncepts spēles skatiem: sākuma ekrāns, varoņa izveides ekrāns un izveidotā varoņa apskatīšana + leģenda. 

Ekrānos izveidota sekojoša funkcionalitāte: 
-- 1.ekrāns:
------ Sākuma ekrāns ar spēles nosaukumu,
------ 2 ievades laukiem - vārdu un vecumu (abi tiek noglabāti atmiņā līdz 3.ekrānam), 
------ poga, lai izietu no spēles,
------ poga, lai turpinātu spēli,
------ toggle mūzikas izslēgšanai/ieslēgšanai.
-- 2.ekrāns:
------ ekrāns 2 daļās - 1. daļā varoņa sprite, 2.daļā apģerbu kolekcijas
------ ekrāna apakšā pogas: lai pārietu uz 3.ekrānu, izietu no spēles, atiestatītu drēbju izvēli
------ ekrāna apakšā dropdown kur izvēlēties starp vīrieti vai sievieti kā savu sprite
------ varoņa skata sānos 2 slider varoņa garuma un platuma mainīšanai
-- 3.ekrāns:
------ Hero legend - mainās atkarībā no izvēlētā varoņa dzimuma
------ Parāda iepriekšējā solī pabeigto varoni
------ Atļauj pāriet uz sākumu vai iziet no spēles
------ No 1.ekrāna ievadlaukiem parāda spēlētāja izvēlēto varoņa vārdu un izrēķina dzimšanas gadu no uzrakstītā vecuma. 

### Tehniskā uzbūve

Skati veidoti apvienojot 2D un 3D skatus proporcijā aptuveni 90% - 2D skati; 10% - 3D renderēšana. 2. un 3. scēnai ir 2 kameras - galvenā 2D eleemntiem un skatiem, un 3D kamera lai nodrošinātu varoņa kustību. 
Kopā ar 3D modulārajiem varoņiem tika ielādēti arī vienkārša humanoīdu kustību paka, un viena no par brīvu esošajām auduma gravitācijas pakām. Diemžēl, šo neizdevās izmantot šī projekta ietvaros ierobežotā 
laika dēļ un problēmām ar shader uzstādījumiem. 

Darbs ir ticis ievietots GitHub un atbilstoši versionēts izveidojot 2 branch un merge ar main branch.
