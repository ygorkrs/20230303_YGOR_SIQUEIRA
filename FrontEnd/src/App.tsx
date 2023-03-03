import "./styles.css";
import { useEffect, useState } from "react";
import { nanoid } from "nanoid";
import { createUser, getHistoryUser } from "./services/userdata.service";
import { IUserHistory } from "./types/UserHistory";
import { IListTagsDto } from "./types/Tag";
import NavBar from "./components/NavBar/NavBar";
import History from "./components/History/History";
import AnimalToRate from "./components/AnimalToRate/AnimalToRate";

const App: React.FC = ({ }) => {

  const [userId, setUserId] = useState<number>(0);
  const [userHistory, setUserHistory] = useState<IUserHistory | null>(null);
  const [listTags, setlistTags] = useState<IListTagsDto | null>(null);
  const [clickAnimalId, setclickAnimalId] = useState<number>(0);

  const fetchData = async () => {
    const data = await getHistoryUser(userId);
    setUserHistory(data)
  }

  useEffect(() => {
    if (userId) {
      fetchData()
    }
  }, [userId, listTags])

  useEffect(() => {
    (async () => {
      const userId = localStorage.getItem('userId') || null;

      if (!userId) {
        const userIdentification = nanoid();
        const tempUserId = await createUser(userIdentification);
        localStorage.setItem('userIdentification', userIdentification);
        localStorage.setItem('userId', tempUserId.toString());
        setUserId(tempUserId)
      } else {
        setUserId(Number(userId));
      }
    })()
  }, [])

  function handleCreateAnimal(animalId: number) {
    setclickAnimalId(0);
  }

  function handleListTagsChange(listTags: IListTagsDto) {
    console.log('handleListTagsChange');
    setlistTags(listTags);
  }

  function handleClickAnimal(animalId: number) {
    setclickAnimalId(animalId);
  }

  return (
    <div className="app">
      <NavBar text="rate_my_animal.com" />
      <div className="full-width-container">
        <div className="left-container">

          {!!userId &&
            <AnimalToRate
              userId={userId}
              onCreateAnimal={handleCreateAnimal}
              onListTagsChange={handleListTagsChange}
              initialClickAnimalId={clickAnimalId}
            />
          }

        </div>
        <div className="right-container">

          {<History
            userHistory={userHistory}
            listTags={listTags}
            onClickAnimal={handleClickAnimal}
          />}

        </div>
      </div>
    </div>
  );
};

export default App;
