pragma solidity ^0.7.3;
//SPDX-License-Identifier: MIT

import "./Oracle888730.sol";

contract Client888730 {
    address public clientOwner;
    address payable public oracleAddress;
    string public a = "0";

    constructor(address payable _oracleAddress){
        clientOwner = msg.sender;
        oracleAddress = _oracleAddress;
    }

    modifier onlyClientOwner() {
        require ( msg.sender == clientOwner, "You are not the owner of the client contract" );
        _;
    }

    modifier onlyOracle() {
        require ( msg.sender == oracleAddress, "You are not the oracle" );
        _;
    }

    event ResponseEvent(
        string value,
        uint _requestType
    ); 

    function GetResponse(string calldata _value, uint _requestType) external payable onlyOracle{
        a = _value;
        emit ResponseEvent(_value, _requestType);
    }

    function SendSubscribeRequest(uint _requestType) public onlyClientOwner{
        Oracle888730 oracle = Oracle888730(oracleAddress);
        oracle.GetSubscribeRequest(_requestType);
    }

    function SendRequest(uint _requestType) public onlyClientOwner{
        Oracle888730 oracle = Oracle888730(oracleAddress);
        oracle.GetRequest(_requestType);
    }

}