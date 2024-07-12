import {ADD_CATEGORY, DELETE_CATEGORY} from "../actions/categoryActionsTypes";

const initState = {
    categories: []
}

function Categories(state = initState, action) {
    switch(action.type) {
        case ADD_CATEGORY:
            const { category } = action.payload;
            return { categories: [...state.categories, category] };
        case DELETE_CATEGORY:
            const { id } = action.payload;
            const categories = state.categories.filter(category => category.id !== id);
            return { categories };
        default:
            return state;
    }
}

export default Categories;