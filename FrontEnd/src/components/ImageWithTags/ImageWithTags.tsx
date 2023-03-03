import React from "react";
import "./ImageWithTags.css";
import { IAnimalHistory } from "../../types/Animal";

interface Props {
  animalHistory: IAnimalHistory,
  onClickAnimal: (animalId: number) => void
}

const ImageWithTags: React.FC<Props> = ({ animalHistory, onClickAnimal }) => {

  function handleOnClickAnimal(event: React.MouseEvent<HTMLDivElement>) {
    const animalId = animalHistory.id;
    onClickAnimal(animalId);
  }

  return (
    <div className="container-history" onClick={handleOnClickAnimal}>
      <img
        src={`data:image/png;base64,${animalHistory.photo}`}
        className="image"
        alt="sample"
        height="128px"
        width="128px"
      />
      <div className="tag-container">
        {animalHistory.tags.map((tag, index) => (
          <span key={index} className="tag tag-old">
            {tag.tagName}
          </span>
        ))}
      </div>
    </div>
  );
};

export default ImageWithTags;
