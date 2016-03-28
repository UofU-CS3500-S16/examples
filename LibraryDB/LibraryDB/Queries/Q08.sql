select Phones.CardNum, Name, Count(Phone) from Phones, Patrons 
  where Phones.CardNum = Patrons.CardNum 
  group by Phones.CardNum, Name
  order by Name