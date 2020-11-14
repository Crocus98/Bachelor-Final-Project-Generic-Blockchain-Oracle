using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace Oracle888730.Contracts.Oracle888730.ContractDefinition
{


    public partial class Oracle888730Deployment : Oracle888730DeploymentBase
    {
        public Oracle888730Deployment() : base(BYTECODE) { }
        public Oracle888730Deployment(string byteCode) : base(byteCode) { }
    }

    public class Oracle888730DeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60c0604052600d60808190526c47656e657269634f7261636c6560981b60a090815261002e9160019190610053565b5034801561003b57600080fd5b50600080546001600160a01b031916331790556100f4565b828054600181600116156101000203166002900490600052602060002090601f01602090048101928261008957600085556100cf565b82601f106100a257805160ff19168380011785556100cf565b828001600101855582156100cf579182015b828111156100cf5782518255916020019190600101906100b4565b506100db9291506100df565b5090565b5b808211156100db57600081556001016100e0565b610688806101036000396000f3fe6080604052600436106100595760003560e01c80630fa4bf721461010f578063238cb73b146101995780635f08ec40146101b85780639b37031714610228578063a578be1614610245578063fdbf746514610276576100d5565b366100d557600080546040516001600160a01b03909116913480156108fc02929091818181858888f19350505050158015610098573d6000803e3d6000fd5b506040805133815234602082015281517f890ae3b9a5e6da809bace04153fc525da24c96e933acfa00a0cc2dcc2f186005929181900390910190a1005b600080546040516001600160a01b03909116913480156108fc02929091818181858888f19350505050158015610098573d6000803e3d6000fd5b34801561011b57600080fd5b5061012461033b565b6040805160208082528351818301528351919283929083019185019080838360005b8381101561015e578181015183820152602001610146565b50505050905090810190601f16801561018b5780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b6101b6600480360360208110156101af57600080fd5b50356103c8565b005b6101b6600480360360208110156101ce57600080fd5b8101906020810181356401000000008111156101e957600080fd5b8201836020820111156101fb57600080fd5b8035906020019184600183028401116401000000008311171561021d57600080fd5b509092509050610406565b6101b66004803603602081101561023e57600080fd5b50356104be565b34801561025157600080fd5b5061025a6104fc565b604080516001600160a01b039092168252519081900360200190f35b34801561028257600080fd5b506101b66004803603606081101561029957600080fd5b6001600160a01b0382351691908101906040810160208201356401000000008111156102c457600080fd5b8201836020820111156102d657600080fd5b803590602001918460018302840111640100000000831117156102f857600080fd5b91908080601f016020809104026020016040519081016040528093929190818152602001838380828437600092019190915250929550509135925061050b915050565b60018054604080516020600284861615610100026000190190941693909304601f810184900484028201840190925281815292918301828280156103c05780601f10610395576101008083540402835291602001916103c0565b820191906000526020600020905b8154815290600101906020018083116103a357829003601f168201915b505050505081565b604080513381526020810183905281517fb12a25da61fe93c9e0502764388a591c98bd87bc11eeedf20e8ebd93dfaec3c6929181900390910190a150565b7f76e164bd47fa8c2042e7d654f6dd9803fc7fad3abdef4f517ee43e76f4b6a9aa3334848460405180856001600160a01b03168152602001848152602001806020018281038252848482818152602001925080828437600083820152604051601f909101601f191690920182900397509095505050505050a1600080546040516001600160a01b03909116913480156108fc02929091818181858888f193505050501580156104b9573d6000803e3d6000fd5b505050565b604080513381526020810183905281517f2e0294094c2fe83c4c8991bc8dd4dbd1cf2e64d3f23e6d854504817c12911f89929181900390910190a150565b6000546001600160a01b031681565b6000546001600160a01b031633146105545760405162461bcd60e51b815260040180806020018281038252602c815260200180610627602c913960400191505060405180910390fd5b60408051634f31474d60e01b8152602481018390526004810191825283516044820152835185926001600160a01b03841692634f31474d92879287929091829160640190602086019080838360005b838110156105bb5781810151838201526020016105a3565b50505050905090810190601f1680156105e85780820380516001836020036101000a031916815260200191505b509350505050600060405180830381600087803b15801561060857600080fd5b505af115801561061c573d6000803e3d6000fd5b505050505050505056fe596f7520617265206e6f7420746865206f776e6572206f6620746865206f7261636c6520636f6e7472616374a2646970667358221220b912057513c690093fdc6289bb86fac20551dd3bd0069f6d591b183d03d1300c64736f6c63430007040033";
        public Oracle888730DeploymentBase() : base(BYTECODE) { }
        public Oracle888730DeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class GetRequestFunction : GetRequestFunctionBase { }

    [Function("GetRequest")]
    public class GetRequestFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_requestType", 1)]
        public virtual BigInteger RequestType { get; set; }
    }

    public partial class GetSubscribeRequestFunction : GetSubscribeRequestFunctionBase { }

    [Function("GetSubscribeRequest")]
    public class GetSubscribeRequestFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "_requestType", 1)]
        public virtual BigInteger RequestType { get; set; }
    }

    public partial class OracleNameFunction : OracleNameFunctionBase { }

    [Function("OracleName", "string")]
    public class OracleNameFunctionBase : FunctionMessage
    {

    }

    public partial class PayServiceFunction : PayServiceFunctionBase { }

    [Function("PayService")]
    public class PayServiceFunctionBase : FunctionMessage
    {
        [Parameter("string", "_service", 1)]
        public virtual string Service { get; set; }
    }

    public partial class SendResponseFunction : SendResponseFunctionBase { }

    [Function("SendResponse")]
    public class SendResponseFunctionBase : FunctionMessage
    {
        [Parameter("address", "_clientAddress", 1)]
        public virtual string ClientAddress { get; set; }
        [Parameter("string", "_value", 2)]
        public virtual string Value { get; set; }
        [Parameter("uint256", "_requestType", 3)]
        public virtual BigInteger RequestType { get; set; }
    }

    public partial class OracleOwnerFunction : OracleOwnerFunctionBase { }

    [Function("oracleOwner", "address")]
    public class OracleOwnerFunctionBase : FunctionMessage
    {

    }

    public partial class GenericPaymentEventEventDTO : GenericPaymentEventEventDTOBase { }

    [Event("GenericPaymentEvent")]
    public class GenericPaymentEventEventDTOBase : IEventDTO
    {
        [Parameter("address", "_sender", 1, false )]
        public virtual string Sender { get; set; }
        [Parameter("uint256", "_value", 2, false )]
        public virtual BigInteger Value { get; set; }
    }

    public partial class RequestEventEventDTO : RequestEventEventDTOBase { }

    [Event("RequestEvent")]
    public class RequestEventEventDTOBase : IEventDTO
    {
        [Parameter("address", "_sender", 1, false )]
        public virtual string Sender { get; set; }
        [Parameter("uint256", "_requestType", 2, false )]
        public virtual BigInteger RequestType { get; set; }
    }

    public partial class ServicePaymentEventEventDTO : ServicePaymentEventEventDTOBase { }

    [Event("ServicePaymentEvent")]
    public class ServicePaymentEventEventDTOBase : IEventDTO
    {
        [Parameter("address", "_sender", 1, false )]
        public virtual string Sender { get; set; }
        [Parameter("uint256", "_value", 2, false )]
        public virtual BigInteger Value { get; set; }
        [Parameter("string", "_serviceType", 3, false )]
        public virtual string ServiceType { get; set; }
    }

    public partial class SubscribeEventEventDTO : SubscribeEventEventDTOBase { }

    [Event("SubscribeEvent")]
    public class SubscribeEventEventDTOBase : IEventDTO
    {
        [Parameter("address", "_sender", 1, false )]
        public virtual string Sender { get; set; }
        [Parameter("uint256", "_requestType", 2, false )]
        public virtual BigInteger RequestType { get; set; }
    }





    public partial class OracleNameOutputDTO : OracleNameOutputDTOBase { }

    [FunctionOutput]
    public class OracleNameOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }





    public partial class OracleOwnerOutputDTO : OracleOwnerOutputDTOBase { }

    [FunctionOutput]
    public class OracleOwnerOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }
}
