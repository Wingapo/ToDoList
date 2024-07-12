import React, { useState } from "react";
import {useDispatch} from "react-redux";
import {addCategory} from "../redux/actions/categoryActions";

const CategoryForm = () => {
    const dispatch = useDispatch();

    const [name, setName] = useState('');
    const handleSubmit = e => {
        e.preventDefault();

        const newCategory = {
            name
        }
        dispatch(addCategory(newCategory));

        setName('');
    }

    return (
        <div className="card mb-2">
            <div className="card-title ms-1">
                <h4>Add category</h4>
            </div>
            <form className="card-body" onSubmit={handleSubmit}>
                <div className="mb-3">
                    <label htmlFor="name" className="form-label">Name</label>
                    <input className="form-control"
                           type="text"
                           id="name"
                           value={name}
                           onChange={(e) => setName(e.target.value)}/>
                </div>
                <button className="btn btn-success float-end" type="submit">Add</button>
            </form>
        </div>
    );
}

export default CategoryForm;