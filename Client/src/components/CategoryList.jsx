import React from "react";
import { useSelector, useDispatch } from "react-redux";
import {deleteCategory} from "../redux/actions/categoryActions";

const CategoryList = () => {
    const dispatch = useDispatch();

    const { categories } = useSelector(state => state.Categories);

    return (
        <div className="card flex-grow-1">
            <div className="card-title ms-1">
                <h4>All Categories</h4>
            </div>
            <div className="card-body">
                <ul className="list-unstyled d-flex gap-2">
                    {categories.map((category) =>
                            <li className="border rounded bg-light px-2 py-1 position-relative"
                                key={category.id}
                                value={category.id}>
                                    {category.name}
                                <button className="btn btn-danger btn-hover"
                                        onClick={() => dispatch(deleteCategory(category.id))}>⨯</button>
                            </li>)}
                </ul>
            </div>
        </div>
    );
}

export default CategoryList;