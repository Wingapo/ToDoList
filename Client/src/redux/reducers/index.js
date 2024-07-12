import { combineReducers } from "redux";
import Tasks from "./tasks";
import Categories from "./categories";

export default combineReducers({
   Tasks,
   Categories
});