import { useState } from "react";
import "./CreateTagModal.css";

interface CreateTagModalProps {
    onClose: () => void;
    onCreateTag: (tagName: string) => void;
}

function CreateTagModal({ onClose, onCreateTag }: CreateTagModalProps) {
    const [tagName, setTagName] = useState("");

    function handleTagNameChange(event: React.ChangeEvent<HTMLInputElement>) {
        setTagName(event.target.value);
    }

    function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
        event.preventDefault();
        onCreateTag(tagName);
    }

    return (
        <div>
            <div className="modal-overlay" onClick={onClose} />
            <div className="modal">
                <h2>Create New Category</h2>
                <form onSubmit={handleSubmit}>
                    <label>
                        Name:
                        <input type="text" value={tagName} onChange={handleTagNameChange} />
                    </label>
                    <button className="btn btn-success" type="submit">Save</button>
                    <button className="btn btn-danger" type="button" onClick={onClose}>Cancel</button>
                </form>
            </div>
        </div>
    );
}

export default CreateTagModal;