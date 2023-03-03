import "./History.css";
import React, { useState, useEffect } from "react";
import ImageWithTags from "../ImageWithTags/ImageWithTags";
import { IUserHistory } from "../../types/UserHistory";
import { IListTagsDto } from "../../types/Tag";
import { IAnimalHistory } from "../../types/Animal";

interface Props {
  userHistory: IUserHistory | null,
  listTags: IListTagsDto | null,
  onClickAnimal: (animalId: number) => void
}

const History: React.FC<Props> = ({ userHistory, listTags, onClickAnimal }) => {
  const [selectedTag, setSelectedTag] = useState<number | undefined>(undefined);
  const [filteredAnimals, setFilteredAnimals] = useState<IAnimalHistory[]>([]);

  const handleTagSelection = (event: React.ChangeEvent<HTMLSelectElement>) => {
    const selectedValue = event.target.value;

    if (selectedValue === "") {
      setSelectedTag(undefined);
    } else {
      setSelectedTag(parseInt(selectedValue));
    }
  };

  useEffect(() => {
    if (selectedTag !== undefined && selectedTag !== 0) {
      const filtered = userHistory?.animals?.filter((animal) =>
        animal.tags.some((tag) => tag.id === selectedTag)
      );
      setFilteredAnimals(filtered || []);
    } else {
      setFilteredAnimals(userHistory?.animals || []);
    }
  }, [selectedTag, userHistory, listTags]);

  function handleClickAnimal(animalId: number) {
    onClickAnimal(animalId);
  }

  return (
    <div className="ImageList">
      <h1>History</h1>
      <div>
        <select value={selectedTag ?? ""} onChange={handleTagSelection}>
          <option value="">Select a category</option>
          {listTags?.listTagsAvailable.map((tag) => (
            <option key={tag.id} value={tag.id}>
              {tag.tagName}
            </option>
          ))}
        </select>
      </div>
      <div className="ImageList-images">
        <div className="container-full">
          {filteredAnimals.length ? (
            filteredAnimals.map((history, index) => (
              <ImageWithTags key={index} animalHistory={history} onClickAnimal={handleClickAnimal} />
            ))
          ) : (
            <p>You don't have history yet</p>
          )}
        </div>
      </div>
    </div>
  );
};

export default History;
