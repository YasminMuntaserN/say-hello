function SearchBar() {
  return (
    <input className={StyledSearchBar} placeholder="Search" type="text"/>
  )
}
const StyledSearchBar ="bg-gray rounded-3xl m-7 p-1 pl-7 w-5/6 shadow-2xl transition-all duration-300";

export default SearchBar
