import "./AnimalToRate.css"
import { useEffect, useState } from "react";
import { getNewAnimalData, getAnimalData, createAnimal, updateAnimal } from "../../services/animaldata.service";
import { createTag, getTagsAvailable } from "../../services/tagdata.service";
import { AnimalDto } from "../../types/Animal";
import { IListTagsDto } from "../../types/Tag";
import CreateTagModal from "../CreateTagModal/CreateTagModal";
import { CiFloppyDisk, CiSquareChevRight } from "react-icons/ci";
import { FaPlus } from "react-icons/fa";
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

interface SelectedTags {
  [tagId: number]: boolean;
}

interface AnimalToRateProps {
  userId: number,
  onCreateAnimal: (animalId: number) => void,
  onListTagsChange: (listTags: IListTagsDto) => void,
  initialClickAnimalId: number,
}

const AnimalToRate: React.FC<AnimalToRateProps> = ({ userId, onCreateAnimal, onListTagsChange, initialClickAnimalId }) => {
  const [animalData, setAnimalData] = useState<AnimalDto | null>(null);
  const [selectedTags, setSelectedTags] = useState<SelectedTags>({});
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [clickedNext, setClickedNext] = useState(false);

  async function fetchAnimalData() {
    const data = await getNewAnimalData();
    setAnimalData(data);
    await onListTagsChange(data.listTagsDto);
  };

  async function fetchIdAnimalData(id: number) {
    const data = await getAnimalData(id);
    if (animalData != null) {
      animalData.animalOriginUrlDto.id = data.originId;
      animalData.photo = data.photo;
      setAnimalData(animalData);

      const updatedSelectedTags: SelectedTags = {};
      data.selectedTags.forEach((tag) => {
        updatedSelectedTags[tag] = !selectedTags[tag];
      });
      setSelectedTags(updatedSelectedTags);
    }
  };

  useEffect(() => {
    console.log('initialClickAnimalId:' + initialClickAnimalId);
    console.log('clickedNext' + clickedNext);

    if (initialClickAnimalId > 0) {
      fetchIdAnimalData(initialClickAnimalId)
        .catch(console.error);
    } else {
      fetchAnimalData()
        .catch(console.error);
    }
  }, [initialClickAnimalId]);

  if (!animalData) {
    return <div className="animal-container">
      <img src={"./load.gif"} className="animal-img" />
    </div>
  }

  function toggleTagSelection(tagId: number) {
    setSelectedTags({
      ...selectedTags,
      [tagId]: !selectedTags[tagId]
    });
  }

  function openModal() {
    setIsModalOpen(true);
  }

  function closeModal() {
    setIsModalOpen(false);
  }

  async function handleCreateTag(tagName: string) {
    const postCreateTagData = async (tagName: string) => {
      const data = await createTag(tagName, userId);
      console.log(data);
    };
    await postCreateTagData(tagName);

    const fetchTagsData = async () => {
      const data = await getTagsAvailable();
      var animal = animalData;
      if (animal) {
        animal.listTagsDto = data;
        setAnimalData(animalData);
        onListTagsChange(data);
      }
    };
    await fetchTagsData();

    setIsModalOpen(false);
  }

  function handleGetNextAnimal() {
    setClickedNext(true);
    getNextAnimal();
  }

  async function getNextAnimal() {
    setAnimalData(null);
    setSelectedTags({});
    await fetchAnimalData();
  }

  async function saveAnimal() {
    const postCreateAnimalData = async (
      origin: number,
      user: number,
      tags: Array<number>,
      photo: string
    ) => {
      return await createAnimal(origin, user, tags, photo);
    };

    const putUpdateData = async (
      id: number,
      user: number,
      tags: Array<number>
    ) => {
      return await updateAnimal(id, user, tags);
    };

    const selectedTagIds = Object.keys(selectedTags)
      .filter((tagId) => selectedTags[Number(tagId)])
      .map(Number);

    if (!animalData)
      return;

    console.log('initialClickAnimalId:' + initialClickAnimalId);
    console.log('clickedNext' + clickedNext);

    if (initialClickAnimalId > 0 && !clickedNext) {
      const newId = await updateAnimal(
        initialClickAnimalId,
        userId,
        selectedTagIds
      );
      setSelectedTags({});
      await onCreateAnimal(newId);
    } else {
      const newId = await createAnimal(
        animalData.animalOriginUrlDto.id,
        userId,
        selectedTagIds,
        animalData.photo
      );
      await getNextAnimal();
    }
    toast.success('Success!');
  }

  return (
    <div className="animal-container">
      <img className="animal-img" src={`data:image/png;base64,${animalData.photo}`} />
      <div className="animal-tag-container">
        {animalData.listTagsDto.listTagsAvailable.map((tag) => (
          <button
            key={tag.id}
            onClick={() => toggleTagSelection(tag.id)}
            className={`tag ${selectedTags[tag.id] ? 'selected' : ''}`}>{tag.tagName}</button>
        ))}
        <button onClick={openModal} className="tag">
          <FaPlus className="icon" /> New Category
        </button>
        {isModalOpen && (
          <CreateTagModal onClose={closeModal} onCreateTag={handleCreateTag} />
        )}
      </div>
      <div>
        <button onClick={handleGetNextAnimal} className="btn">
          Next <CiSquareChevRight className="icon" />
        </button>
        <button onClick={saveAnimal} className="btn">
          Save <CiFloppyDisk className="icon" />
        </button>
      </div>
      <ToastContainer position="bottom-left" />
    </div>
  );
}

export default AnimalToRate;